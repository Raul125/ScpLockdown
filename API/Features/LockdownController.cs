using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using SCPLockdown.API.EventArgs;
using SCPLockdown.API.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SCPLockdown.API.Features
{
    public static class LockdownController
    {
        public static bool IsScp079LockedUp { get; private set; } = false;
        public static bool IsScp096LockedUp { get; private set; } = false;
        public static bool IsScp106LockedUp { get; private set; } = false;
        public static bool IsScp049LockedUp { get; private set; } = false;
        public static bool IsScp939LockedUp { get; private set; } = false;
        public static bool IsScp173LockedUp { get; private set; } = false;

        public static void ToggleLockedUpState(RoleType role)
        {
            switch (role)
            {
                case RoleType.Scp079:
                    IsScp079LockedUp = !IsScp079LockedUp;
                    break;
                case RoleType.Scp096:
                    IsScp096LockedUp = !IsScp096LockedUp;
                    break;
                case RoleType.Scp106:
                    IsScp106LockedUp = !IsScp106LockedUp;
                    break;
                case RoleType.Scp049:
                    IsScp049LockedUp = !IsScp049LockedUp;
                    break;
                case RoleType.Scp173:
                    IsScp173LockedUp = !IsScp173LockedUp;
                    break;
                case RoleType.Scp93953:
                case RoleType.Scp93989:
                    IsScp939LockedUp = !IsScp939LockedUp;
                    break;
            }
        }

        public static void ResetAllStates()
        {
            IsScp079LockedUp = false;
            IsScp096LockedUp = false;
            IsScp106LockedUp = false;
            IsScp049LockedUp = false;
            IsScp939LockedUp = false;
            IsScp173LockedUp = false;
        }

        // Lock
        public static void Lockdown106(int time)
        {
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp106))
            {
                player.SendToPocketDimension();
            }

            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock106s(time)));
        }

        public static void Lockdown049(int time)
        {
            EventHandlers.Scp049Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock049(time)));
        }

        public static void Lockdown096(int time)
        {
            EventHandlers.Scp096Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock096(time)));
        }

        public static void Lockdown173(int time)
        {
            // Using this door because the gate is opened by the base game
            EventHandlers.Scp173Door.ChangeLock(DoorLockType.SpecialDoorFeature);
            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock173(time)));
        }

        public static void Lockdown939(int time)
        {
            foreach (var door in EventHandlers.Scp939Doors)
            {
                door.ChangeLock(DoorLockType.SpecialDoorFeature);
            }

            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock939s(time)));
        }

        // Unlock
        public static IEnumerator<float> Unlock106s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp106.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp106, state, !state);
            if (!ev.IsAllowed)
                yield break;

            var pos = RoleType.Scp106.GetRandomSpawnProperties().Item1;
            ToggleLockedUpState(RoleType.Scp106);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp106))
            {
                player.Position = pos;
                player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock079(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp079.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp079, state, !state);
            if (!ev.IsAllowed)
                yield break;

            ToggleLockedUpState(RoleType.Scp079);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp079))
            {
                player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock049(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp049.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp049, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp049Door.Unlock();
            ToggleLockedUpState(RoleType.Scp049);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp049))
            {
                player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock096(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp096.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp096, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp096Door.Unlock();
            ToggleLockedUpState(RoleType.Scp096);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp096))
            {
                player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock173(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp173.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp173, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp173Door.Unlock();
            ToggleLockedUpState(RoleType.Scp173);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp173))
            {
                player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock939s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleType.Scp93953.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleType.Scp93953, state, !state);
            if (!ev.IsAllowed)
                yield break;

            foreach (var door in EventHandlers.Scp939Doors)
            {
                door.Unlock();
            }

            ToggleLockedUpState(RoleType.Scp93953);
            foreach (var player in Player.List.Where(x => x.Role.Is939()))
            {
                player.SendContainmentBreachText();
            }
        }
    }
}
