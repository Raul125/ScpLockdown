using Exiled.API.Enums;
using Exiled.API.Features;
using Interactables.Interobjects.DoorUtils;
using MEC;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;

namespace ScpLockdown.Helper
{
    public class Methods
    {
        private ScpLockdown plugin;
        public Methods(ScpLockdown scplockdown)
        {
            plugin = scplockdown;
        }

        public void Lockdown106(KeyValuePair<RoleType, int> scp)
        {
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp106))
            {
                player.SendToPocketDimension();
            }

            plugin.EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock106s(scp.Value)));
        }

        public IEnumerator<float> Unlock106s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var pos = RoleType.Scp106.GetRandomSpawnProperties().Item1;
            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp106);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp106))
            {
                player.Position = pos;
                player.SendContainmentBreachText();
            }
        }

        public IEnumerator<float> Unlock079(int time)
        {
            yield return Timing.WaitForSeconds(time);
            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp079);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp079))
            {
                player.SendContainmentBreachText();
            }
        }

        public void Lockdown049(KeyValuePair<RoleType, int> scp)
        {
            plugin.EventHandlers.Scp049Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            plugin.EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock049(scp.Value)));
        }

        public IEnumerator<float> Unlock049(int time)
        {
            yield return Timing.WaitForSeconds(time);
            plugin.EventHandlers.Scp049Door.Unlock();
            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp049);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp049))
            {
                player.SendContainmentBreachText();
            }
        }

        public void Lockdown096(KeyValuePair<RoleType, int> scp)
        {
            plugin.EventHandlers.Scp096Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            plugin.EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock096(scp.Value)));
        }

        public IEnumerator<float> Unlock096(int time)
        {
            yield return Timing.WaitForSeconds(time);
            plugin.EventHandlers.Scp096Door.Unlock();
            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp096);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp096))
            {
                player.SendContainmentBreachText();
            }
        }

        public void Lockdown173(KeyValuePair<RoleType, int> scp)
        {
            // Using this door because the gate is opened by the base game
            plugin.EventHandlers.Scp173Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            plugin.EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock173(scp.Value)));
        }

        public IEnumerator<float> Unlock173(int time)
        {
            yield return Timing.WaitForSeconds(time);
            plugin.EventHandlers.Scp173Door.Unlock();
            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp173);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp173))
            {
                player.SendContainmentBreachText();
            }
        }

        public void Lockdown939(KeyValuePair<RoleType, int> scp)
        {
            foreach (var door in plugin.EventHandlers.Scp939Doors)
            {
                door.ChangeLock(DoorLockType.SpecialDoorFeature);
            }

            plugin.EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock939(scp.Value)));
        }

        public IEnumerator<float> Unlock939(int time)
        {
            yield return Timing.WaitForSeconds(time);
            foreach (var door in plugin.EventHandlers.Scp939Doors)
            {
                door.Unlock();
            }

            plugin.EventHandlers.LockdownStates.ToggleLockedUpState(RoleType.Scp93953);
            foreach (var player in Player.List.Where(x => x.Role.Is939()))
            {
                player.SendContainmentBreachText();
            }
        }

        public void SendCassies()
        {
            foreach (var cassie in plugin.Config.ParsedCassies)
            {
                plugin.EventHandlers.RunningCoroutines.Add(Timing.CallDelayed(cassie.Item2, () =>
                {
                    Cassie.Message(cassie.Item1, false, false);
                }));
            }
        }

        public void LockAffectedDoors()
        {
            foreach (var door in plugin.EventHandlers.DoorsToLock)
            {
                door.Key.ChangeLock(DoorLockType.SpecialDoorFeature);
            }
        }

        public void UnLockAffectedDoors()
        {
            foreach (var door in plugin.EventHandlers.DoorsToLock)
            {
                plugin.EventHandlers.RunningCoroutines.Add(Timing.CallDelayed(door.Value, () =>
                {
                    door.Key.Unlock();
                }));
            }
        }
    }
}
