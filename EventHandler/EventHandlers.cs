namespace SCPLockdown
{
    using System.Collections.Generic;
    using MEC;
    using Exiled.API.Features;
    using Exiled.API.Enums;
    using API.Extensions;
    using API.Features;
    using API.EventArgs;
    using PlayerRoles;
    using Exiled.Events.EventArgs.Server;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Scp106;
    using Exiled.Events.EventArgs.Scp079;
    using System.Linq;
    using Exiled.API.Features.Doors;

    public class EventHandlers
    {
        private readonly SCPLockdown plugin;

        public static List<CoroutineHandle> RunningCoroutines = new List<CoroutineHandle>();

        // Scps Doors
        public static List<Door> Scp939Doors = new List<Door>();
        public static Door Scp049Door;
        public static Door Scp173Door;
        public static Door Scp096Door;

        public EventHandlers(SCPLockdown scplockdown)
        {
            plugin = scplockdown;
        }

        public void OnWaitingForPlayers()
        {
            foreach (Door door in Room.Get(RoomType.Hcz939).Doors)
                Scp939Doors.Add(door);

            Scp096Door = Room.Get(RoomType.Hcz096).Doors.First(x => x.Type != DoorType.Scp096);
            Scp049Door = Door.Get(DoorType.Scp049Gate);
            Scp173Door = Door.Get(DoorType.Scp173NewGate);

            foreach (AffectedDoor affectedoor in plugin.Config.AffectedDoors)
            {
                affectedoor.Doors.Clear();
                foreach (Door door in Door.List)
                {
                    if (door.Type == affectedoor.DoorType)
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
                    var state = scp.RoleType.LockedUpState();
                    var ev = new TogglingLockedUpStateEventArgs(scp.RoleType, state, !state);
                    if (!ev.IsAllowed)
                        continue;

                    LockdownController.ToggleLockedUpState(scp.RoleType);
                    switch (scp.RoleType)
                    {
                        case RoleTypeId.Scp079:
                            {
                                RunningCoroutines.Add(Timing.RunCoroutine(LockdownController.Unlock079(scp.TimeToUnlock)));
                                break;
                            }
                        case RoleTypeId.Scp173:
                            {
                                LockdownController.Lockdown173(scp.TimeToUnlock);
                                break;
                            }
                        case RoleTypeId.Scp106:
                            {
                                LockdownController.Lockdown106(scp.TimeToUnlock);
                                break;
                            }
                        case RoleTypeId.Scp049:
                            {
                                LockdownController.Lockdown049(scp.TimeToUnlock);
                                break;
                            }
                        case RoleTypeId.Scp096:
                            {
                                LockdownController.Lockdown096(scp.TimeToUnlock);
                                break;
                            }
                        case RoleTypeId.Scp939:
                            {
 
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
                Timing.KillCoroutines(coroutine);

            RunningCoroutines.Clear();
        }

        public void OnRoundRestarting()
        {
            // This prevents us from having unwanted coroutines running
            foreach (CoroutineHandle coroutine in RunningCoroutines)
                Timing.KillCoroutines(coroutine);

            Scp049Door = null;
            Scp173Door = null;
            Scp096Door = null;

            Scp939Doors.Clear();
            RunningCoroutines.Clear();

            LockdownController.ResetAllStates();
        }

        // Scp 106 Part
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            // This makes Scp106 lockdown compatible with scpswap
            if (LockdownController.IsScp106LockedUp && ev.NewRole == RoleTypeId.Scp106)
            {
                RunningCoroutines.Add(Timing.CallDelayed(1, () =>
                {
                    if (ev.Player.Role.Type is RoleTypeId.Scp106)
                        ev.Player.SendToPocketDimension();
                }));
            }
        }

        public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            if (ev.Player.Role.Type is RoleTypeId.Scp106 && LockdownController.IsScp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }

        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            if (ev.Player.Role.Type is RoleTypeId.Scp106 && LockdownController.IsScp106LockedUp)
            {
                ev.Player.SendToPocketDimension();
                ev.IsAllowed = false;
            }
        }


        public void OnTeleporting(TeleportingEventArgs ev)
        {
            if (LockdownController.IsScp106LockedUp)
                ev.IsAllowed = false;
        }

        // Scp 079 Part
        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp)
                ev.IsAllowed = false;
        }

        public void OnInteractingDoor(TriggeringDoorEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && ev.Player.Role.Type is RoleTypeId.Scp079)
                ev.IsAllowed = false;
        }

        public void OnChangingCamera(ChangingCameraEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && !plugin.Config.Scp079Camera)
                ev.IsAllowed = false;
        }

        public void OnElevatorTeleport(ElevatorTeleportingEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp && !plugin.Config.Scp079Camera)
                ev.IsAllowed = false;
        }

        public void OnChangingSpeakerStatus(ChangingSpeakerStatusEventArgs ev)
        {
            if (LockdownController.IsScp079LockedUp)
                ev.IsAllowed = false;
        }
    }
}
