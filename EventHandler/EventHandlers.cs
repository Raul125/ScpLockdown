using ScpLockdown.Helper;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.Events.EventArgs;
using ScpLockdown.States;
using Exiled.API.Features;
using Exiled.API.Enums;

namespace ScpLockdown
{
    public class EventHandlers
    {
        private ScpLockdown plugin;
        public Methods Methods;
        public LockdownStates LockdownStates;

        public List<CoroutineHandle> RunningCoroutines;
        public Dictionary<Door, int> DoorsToLock;

        // Scps Doors
        public List<Door> Scp939Doors;
        public Door Scp049Door;
        public Door Scp173Door;
        public Door Scp096Door;

        public EventHandlers(ScpLockdown scplockdown)
        {
            plugin = scplockdown;
            Methods = new Methods(scplockdown);
            LockdownStates = new LockdownStates();
            RunningCoroutines = new List<CoroutineHandle>();
            DoorsToLock = new Dictionary<Door, int>();
            Scp939Doors = new List<Door>();
        }

        public void OnWaitingForPlayers()
        {
            // Scp939 Doors
            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);
            Scp939Doors.Add(Map.Doors.GetClosestDoor(room939));
            Scp939Doors.Add(Map.Doors.GetClosestDoor(room939, false, Scp939Doors));

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
                foreach (var door in Map.Doors.Where(x => x.Type == affectedoor.Key))
                {
                    DoorsToLock.Add(door, affectedoor.Value);
                }
            }

            Methods.LockAffectedDoors();
        }

        public void OnRoundStart()
        {
            Methods.UnLockAffectedDoors();
            Methods.SendCassies();

            RunningCoroutines.Add(Timing.CallDelayed(1, () =>
            {
                foreach (var scp in plugin.Config.AffectedScps)
                {
                    LockdownStates.ToggleLockedUpState(scp.Key);

                    switch (scp.Key)
                    {
                        case RoleType.Scp079:
                            RunningCoroutines.Add(Timing.RunCoroutine(Methods.Unlock079(scp.Value)));
                            break;
                        case RoleType.Scp173:
                            Methods.Lockdown173(scp);
                            break;
                        case RoleType.Scp106:
                            Methods.Lockdown106(scp);
                            break;
                        case RoleType.Scp049:
                            Methods.Lockdown049(scp);
                            break;
                        case RoleType.Scp096:
                            Methods.Lockdown096(scp);
                            break;
                        case RoleType.Scp93953:
                        case RoleType.Scp93989:
                            Methods.Lockdown939(scp);
                            break;
                    }
                }
            }));
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            // This makes Scp106 lockdown compatible with scpswap
            if (LockdownStates.Scp106LockedUp && ev.NewRole == RoleType.Scp106)
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
            DoorsToLock.Clear();
            LockdownStates.ResetAllStates();
            RunningCoroutines.Clear();
        }

        // Scp 106 Part
        public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp106 && LockdownStates.Scp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }

        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp106 && LockdownStates.Scp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }

        public void OnCreatingPortal(CreatingPortalEventArgs ev)
        {
            if (LockdownStates.Scp106LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnTeleporting(TeleportingEventArgs ev)
        {
            if (LockdownStates.Scp106LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        // Scp 079 Part
        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnInteractingDoor(TriggeringDoorEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp && ev.Player.Role == RoleType.Scp079)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnChangingCamera(ChangingCameraEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp && !plugin.Config.Scp079Camera)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnElevatorTeleport(ElevatorTeleportingEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp && !plugin.Config.Scp079Camera)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnStartingSpeaker(StartingSpeakerEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp)
            {
                ev.IsAllowed = false;
            }
        }
    }
}
