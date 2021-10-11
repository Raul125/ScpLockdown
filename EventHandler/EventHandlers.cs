using ScpLockdown.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.Events.EventArgs;
using ScpLockdown.States;
using Interactables.Interobjects.DoorUtils;
using System.Collections.ObjectModel;
using Exiled.API.Features;
using Exiled.API.Extensions;
using Exiled.API.Enums;

namespace ScpLockdown
{
    public class EventHandlers
    {
        private ScpLockdown plugin;
        public Methods Methods;
        public LockdownStates LockdownStates;
        public List<CoroutineHandle> RunningCoroutines;

        // Doors
        public List<Door> ClassDDoors = new List<Door>();
        public List<Door> Doors939 = new List<Door>();

        public EventHandlers(ScpLockdown scplockdown)
        {
            plugin = scplockdown;
            Methods = new Methods(scplockdown);
            LockdownStates = new LockdownStates();
            RunningCoroutines = new List<CoroutineHandle>();
        }

        public void OnWaitingForPlayers()
        {
            ResetAllStates();
            Doors939.Clear();

            // ClassD Doors
            if (plugin.Config.CheckedLockedDoors > 0)
            {
                ClassDDoors = Map.Doors.Where(x => x.Base.name.StartsWith("Prison")).ToList();

                foreach (var door in ClassDDoors)
                {
                    door.ChangeLock(DoorLockType.SpecialDoorFeature);
                }
            }

            // 939 Doors
            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);
            Doors939.Add(Map.Doors.GetClosestDoor(room939));
            Doors939.Add(Map.Doors.GetClosestDoor(room939, false, Doors939));
        }

        public void OnRoundStart()
        {
            if (plugin.Config.ClassDLock > 0)
            {
                RunningCoroutines.Add(Timing.RunCoroutine(Methods.OpenDoorsAfterTime()));
            }

            if (plugin.Config.CassieTime > 0)
            {
                RunningCoroutines.Add(Timing.RunCoroutine(Methods.CassieMsg()));
            }

            foreach (var doortype in plugin.Config.LockedDoors)
            {
                var door = Map.Doors.First(x => x.Type == doortype.Key);
                door.ChangeLock(DoorLockType.SpecialDoorFeature);
                RunningCoroutines.Add(Timing.CallDelayed(doortype.Value, () =>
                {
                    door.Unlock();
                }));
            }

            Timing.CallDelayed(1, () =>
            {
                foreach (KeyValuePair<RoleType, int> scp in configScpList)
                {
                    _lockdownStates.ToggleLockedUpState(scp.Key);

                    switch (scp.Key)
                    {
                        case RoleType.Scp079:
                            runningCoroutines.Add(Timing.RunCoroutine(Methods.Unlock079s(scp.Value)));
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
            });
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            // This makes Scp106 lockdown compatible with scpswap
            if (LockdownStates.Scp106LockedUp == true && ev.NewRole == RoleType.Scp106)
            {
                Timing.CallDelayed(1, () =>
                {
                    if (ev.Player.Role == RoleType.Scp106)
                    {
                        Methods.LockSingle106(ev.Player);
                    }
                });
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            foreach (CoroutineHandle coroutine in runningCoroutines)
            {
                Timing.KillCoroutines(coroutine);
            }

            runningCoroutines.Clear();
        }

        public void OnRoundRestarting()
        {
            // This prevents us from having unwanted coroutines running
            foreach (CoroutineHandle coroutine in runningCoroutines)
            {
                Timing.KillCoroutines(coroutine);
            }

            runningCoroutines.Clear();
        }

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

        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            if (LockdownStates.Scp079LockedUp)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            // Using Player event because 079 event isn't working, idk why
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

        public void ResetAllStates()
        {
            _lockdownStates.ResetAllStates();
        }
    }
}
