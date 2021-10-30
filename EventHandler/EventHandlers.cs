using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Exiled.API.Enums;
using SCPLockdown.API.Extensions;
using SCPLockdown.API.Features;
using SCPLockdown.API.EventArgs;

namespace SCPLockdown
{
    public class EventHandlers
    {
        private readonly SCPLockdown plugin;

        public static List<CoroutineHandle> RunningCoroutines = new List<CoroutineHandle>();

        // Scps Doors
        public static List<Door> Scp939Doors = new List<Door>();
        public static Door Scp049Door;
        public static Door Scp173Door;
        public static Door Scp096Door;

        private bool Scps939Locked = false;

        public EventHandlers(SCPLockdown scplockdown)
        {
            plugin = scplockdown;
        }

        public void OnWaitingForPlayers()
        {
            // Scp939 Doors
            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);
            var firstscp939door = Map.Doors.GetClosestDoor(room939);
            Scp939Doors.Add(firstscp939door);
            Scp939Doors.Add(Map.Doors.GetClosestDoor(room939, firstscp939door, false));

            // Scp096 Door
            var door096 = Map.GetDoorByName("096");
            Scp096Door = Map.Doors.GetClosestDoor(door096);

            // Scp049 Door
            var door049 = Map.GetDoorByName("049_ARMORY");
            Scp049Door = Map.Doors.GetClosestDoor(door049);

            // Scp173 Door
            Scp173Door = Map.GetDoorByName("173_CONNECTOR");

            foreach (var affectedoor in plugin.Config.AffectedDoors)
            {
                affectedoor.Doors.Clear();
                foreach (var door in Map.Doors.Where(x => x.Type == affectedoor.DoorType))
                {
                    affectedoor.Doors.Add(door);
                }
            }

            Methods.LockAffectedDoors();
        }

        public void OnRoundStart()
        {
            Methods.ProcessDoors();
            Methods.SendCassies();

            RunningCoroutines.Add(Timing.CallDelayed(1, () =>
            {
                foreach (var scp in plugin.Config.AffectedScps)
                {
                    if (Scps939Locked && scp.RoleType.Is939())
                        continue;

                    var state = scp.RoleType.LockedUpState();
                    var ev = new TogglingLockedUpStateEventArgs(scp.RoleType, state, !state);
                    if (!ev.IsAllowed)
                        continue;

                    LockdownController.ToggleLockedUpState(scp.RoleType);
                    switch (scp.RoleType)
                    {
                        case RoleType.Scp079:
                            {
                                RunningCoroutines.Add(Timing.RunCoroutine(LockdownController.Unlock079(scp.TimeToUnlock)));
                                break;
                            }
                        case RoleType.Scp173:
                            {
                                LockdownController.Lockdown173(scp.TimeToUnlock);
                                break;
                            }
                        case RoleType.Scp106:
                            {
                                LockdownController.Lockdown106(scp.TimeToUnlock);
                                break;
                            }
                        case RoleType.Scp049:
                            {
                                LockdownController.Lockdown049(scp.TimeToUnlock);
                                break;
                            }
                        case RoleType.Scp096:
                            {
                                LockdownController.Lockdown096(scp.TimeToUnlock);
                                break;
                            }
                        case RoleType.Scp93989:
                        case RoleType.Scp93953:
                            {
                                Scps939Locked = true;
                                LockdownController.Lockdown939(scp.TimeToUnlock);
                                break;
                            }
                    }
                }
            }));
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            foreach (CoroutineHandle coroutine in RunningCoroutines)
            {
                Timing.KillCoroutines(coroutine);
            }

            RunningCoroutines.Clear();
        }

        public void OnRoundRestarting()
        {
            // This prevents us from having unwanted coroutines running
            foreach (CoroutineHandle coroutine in RunningCoroutines)
            {
                Timing.KillCoroutines(coroutine);
            }

            Scp049Door = null;
            Scp173Door = null;
            Scp096Door = null;
            Scp939Doors.Clear();
            LockdownController.ResetAllStates();
            RunningCoroutines.Clear();
            Scps939Locked = false;
        }

        // Scp 106 Part
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            // This makes Scp106 lockdown compatible with scpswap
            if (LockdownController.IsScp106LockedUp && ev.NewRole == RoleType.Scp106)
            {
                RunningCoroutines.Add(Timing.CallDelayed(1, () =>
                {
                    if (ev.Player.Role == RoleType.Scp106)
                    {
                        ev.Player.SendToPocketDimension();
                    }
                }));
            }
        }

        public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp106 && LockdownController.IsScp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }

        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp106 && LockdownController.IsScp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }

        public void OnCreatingPortal(CreatingPortalEventArgs ev)
        {
            if (LockdownController.IsScp106LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnTeleporting(TeleportingEventArgs ev)
        {
            if (LockdownController.IsScp106LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        // Scp 079 Part
        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnInteractingDoor(TriggeringDoorEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && ev.Player.Role == RoleType.Scp079)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnChangingCamera(ChangingCameraEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && !plugin.Config.Scp079Camera)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnElevatorTeleport(ElevatorTeleportingEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && !plugin.Config.Scp079Camera)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnStartingSpeaker(StartingSpeakerEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp)
            {
                ev.IsAllowed = false;
            }
        }
    }
}
