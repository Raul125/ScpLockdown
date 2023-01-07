namespace SCPLockdown.API.Features
{
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using MEC;
    using EventArgs;
    using Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using PlayerRoles;

    public static class LockdownController
    {
        public static bool IsScp079LockedUp { get; private set; } = false;
        public static bool IsScp096LockedUp { get; private set; } = false;
        public static bool IsScp106LockedUp { get; private set; } = false;
        public static bool IsScp049LockedUp { get; private set; } = false;
        public static bool IsScp939LockedUp { get; private set; } = false;
        public static bool IsScp173LockedUp { get; private set; } = false;

        public static void ToggleLockedUpState(RoleTypeId role)
        {
            switch (role)
            {
                case RoleTypeId.Scp079:
                    IsScp079LockedUp = !IsScp079LockedUp;
                    break;
                case RoleTypeId.Scp096:
                    IsScp096LockedUp = !IsScp096LockedUp;
                    break;
                case RoleTypeId.Scp106:
                    IsScp106LockedUp = !IsScp106LockedUp;
                    break;
                case RoleTypeId.Scp049:
                    IsScp049LockedUp = !IsScp049LockedUp;
                    break;
                case RoleTypeId.Scp173:
                    IsScp173LockedUp = !IsScp173LockedUp;
                    break;
                case RoleTypeId.Scp939:
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
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp106)
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
                door.ChangeLock(DoorLockType.SpecialDoorFeature);

            EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(Unlock939s(time)));
        }

        // Unlock
        public static IEnumerator<float> Unlock106s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp106.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp106, state, !state);
            if (!ev.IsAllowed)
                yield break;

            var pos = RoleTypeId.Scp106.GetRandomSpawnLocation().Position;
            ToggleLockedUpState(RoleTypeId.Scp106);
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp106)
                {
                    player.Position = pos;
                    player.SendContainmentBreachText();
                }
            }
        }

        public static IEnumerator<float> Unlock079(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp079.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp079, state, !state);
            if (!ev.IsAllowed)
                yield break;

            ToggleLockedUpState(RoleTypeId.Scp079);
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp079)
                    player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock049(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp049.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp049, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp049Door.Unlock();
            ToggleLockedUpState(RoleTypeId.Scp049);
            foreach (var player in Player.List)
            {
                if (player.Role.Type == RoleTypeId.Scp049)
                    player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock096(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp096.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp096, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp096Door.Unlock();
            ToggleLockedUpState(RoleTypeId.Scp096);
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp096)
                    player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock173(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp173.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp173, state, !state);
            if (!ev.IsAllowed)
                yield break;

            EventHandlers.Scp173Door.Unlock();
            ToggleLockedUpState(RoleTypeId.Scp173);
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp173)
                    player.SendContainmentBreachText();
            }
        }

        public static IEnumerator<float> Unlock939s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var state = RoleTypeId.Scp939.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(RoleTypeId.Scp939, state, !state);
            if (!ev.IsAllowed)
                yield break;

            foreach (var door in EventHandlers.Scp939Doors)
                door.Unlock();

            ToggleLockedUpState(RoleTypeId.Scp939);
            foreach (var player in Player.List)
            {
                if (player.Role.Type is RoleTypeId.Scp939)
                    player.SendContainmentBreachText();
            }
        }
    }
}
