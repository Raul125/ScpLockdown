using Exiled.API.Features;
using ScpLockdown.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.Events.EventArgs;
using ScpLockdown.States;
using Interactables.Interobjects.DoorUtils;
using System.Collections.ObjectModel;

namespace ScpLockdown.EventHandlers
{
    public class RoundHandler
    {
        public LockdownStates _lockdownStates;
        public List<KeyValuePair<Player, CoroutineHandle>> larryCoroutines;
        public List<CoroutineHandle> runningCoroutines;
        public ScpLockdown plugin;
        public List<DoorVariant> doorsdb { get; } = new List<DoorVariant>();

        public RoundHandler(ScpLockdown plugin)
        {
            this.plugin = plugin;
            _lockdownStates = new LockdownStates();
            runningCoroutines = new List<CoroutineHandle>();
            larryCoroutines = new List<KeyValuePair<Player, CoroutineHandle>>();
        }

        public void OnRoundStart()
        {
            Dictionary<RoleType, int> configScpList = new Dictionary<RoleType, int>();

            if (plugin.Config.ClassDLock != 0)
            {
                Timing.RunCoroutine(plugin.methods.OpenDoorsAfterTime());
            }

            if (plugin.Config.CassieTime != 0)
            {
                Timing.RunCoroutine(plugin.methods.CassieMsg());
            }

            //Filter unique RoleTypes
            foreach (var entry in plugin.Config.AffectedScps)
            {
                if (!configScpList.Select(x => x.Key).Contains(entry.Key))
                {
                    configScpList.Add(entry.Key, entry.Value);
                }
            }

            runningCoroutines.Add(Timing.CallDelayed(1, () =>
            {
                foreach (KeyValuePair<RoleType, int> scp in configScpList)
                {
                    _lockdownStates.ToggleLockedUpState(scp.Key);

                    switch (scp.Key)
                    {
                        case RoleType.Scp079:
                            plugin.methods.Lockdown079(scp);
                            break;
                        case RoleType.Scp173:
                            plugin.methods.Lockdown173(scp);
                            break;
                        case RoleType.Scp106:
                            plugin.methods.Lockdown106(scp);
                            break;
                        case RoleType.Scp049:
                            plugin.methods.Lockdown049(scp);
                            break;
                        case RoleType.Scp096:
                            plugin.methods.Lockdown096(scp);
                            break;
                        case RoleType.Scp93953:
                        case RoleType.Scp93989:
                            plugin.methods.Lockdown939(scp);
                            break;
                    }
                }
            }));
        }

        public void OnWaitingForPlayers()
        {
            if (plugin.Config.ClassDLock != 0)
            {
                this.doorsdb.Clear();
                ReadOnlyCollection<DoorVariant> doors = Exiled.API.Features.Map.Doors;
                int num = doors.Count<DoorVariant>();
                for (int i = 0; i < num; i++)
                {
                    DoorVariant doorVariant = doors[i];
                    if (doorVariant.name.StartsWith("Prison"))
                    {
                        doorVariant.ServerChangeLock(DoorLockReason.AdminCommand, true);
                        this.doorsdb.Add(doorVariant);
                    }
                }
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (larryCoroutines.Select((x) => x.Key).Contains(ev.Player))
            {
                larryCoroutines.Where((x) => x.Key == ev.Player).Select((x) => x.Value).ToList().ForEach((x) => Timing.KillCoroutines(x));
                larryCoroutines.RemoveAt(larryCoroutines.FindIndex((x) => x.Key == ev.Player));
                ev.Player.IsGodModeEnabled = false;
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            runningCoroutines.ForEach(x => Timing.KillCoroutines(x));
            larryCoroutines.Select((x) => x.Value).ToList().ForEach((x) => Timing.KillCoroutines(x));
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
            if (LockdownStates.Scp079LockedUp)
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

        public void OnElevatorTeleport(ElevatorTeleportEventArgs ev)
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
