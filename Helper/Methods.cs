using Exiled.API.Enums;
using Exiled.API.Features;
using Interactables.Interobjects.DoorUtils;
using MEC;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;

namespace ScpLockdown.Helper
{
    public static class Methods
    {
        public static void Lockdown106(KeyValuePair<RoleType, int> scp)
        {
            foreach (var player in Player.List.Where(e => e.Role == RoleType.Scp106))
            {
                player.SendToPocketDimension();
                player.IsGodModeEnabled = true;
            }
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock106s(scp.Value)));
        }

        public static void LockSingle106(Player ply)
        {
            ply.IsGodModeEnabled = true;
            ply.SendToPocketDimension();
        }

        public static IEnumerator<float> Unlock106s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            var pos = RoleType.Scp106.GetRandomSpawnProperties().Item1;
            foreach (var player in Player.List.Where(e => e.Role == RoleType.Scp106))
            {
                player.Position = pos;
                player.IsGodModeEnabled = false;
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp106);
        }

        public static IEnumerator<float> Unlock079s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp079))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp079);
        }

        public static void Lockdown049(KeyValuePair<RoleType, int> scp)
        {
            var door049 = Map.GetDoorByName("049_ARMORY");
            var heavyDoor049 = Map.Doors.GetClosestDoor(door049);
            heavyDoor049.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock049(scp.Value, heavyDoor049)));
        }

        public static IEnumerator<float> Unlock049(int time, Exiled.API.Features.Door door)
        {
            yield return Timing.WaitForSeconds(time);
            door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp049);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp049))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public static void Lockdown096(KeyValuePair<RoleType, int> scp)
        {
            var door096 = Map.GetDoorByName("096");
            Exiled.API.Features.Door nearestDoor = Map.Doors.GetClosestDoor(door096);
            nearestDoor.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock096(scp.Value, nearestDoor)));
        }

        public static IEnumerator<float> Unlock096(int time, Exiled.API.Features.Door door)
        {
            yield return Timing.WaitForSeconds(time);
            door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp096);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp096))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public static void Lockdown173(KeyValuePair<RoleType, int> scp)
        {
            // Using this door because the gate is opened by the base game
            Exiled.API.Features.Door door173 = Map.GetDoorByName("173_CONNECTOR");
            door173.Base.TargetState = false;
            door173.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock096(scp.Value, door173)));
        }

        public static IEnumerator<float> Unlock173(int time, DoorVariant door)
        {
            yield return Timing.WaitForSeconds(time);
            door.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp173);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp173))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public static void Lockdown939(KeyValuePair<RoleType, int> scp)
        {
            List<Exiled.API.Features.Door> targetDoors = new List<Exiled.API.Features.Door>();
            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);
            targetDoors.Add(Map.Doors.GetClosestDoor(room939));
            targetDoors.Add(Map.Doors.GetClosestDoor(room939, false, targetDoors));
            foreach (var door in targetDoors)
            {
                door.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            }
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock939(scp.Value, targetDoors)));
        }

        public static IEnumerator<float> Unlock939(int time, List<Exiled.API.Features.Door> doors)
        {
            yield return Timing.WaitForSeconds(time);

            foreach (var door in doors)
            {
                door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
            }

            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp93953);

            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp93953 || x.Role == RoleType.Scp93989))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public static IEnumerator<float> OpenDoorsAfterTime()
        {
            yield return Timing.WaitForSeconds(ScpLockdown.Instance.Config.ClassDLock);
            OpenClassDDoors();
        }

        public static void OpenClassDDoors()
        {
            int count = ScpLockdown.Instance.Handler.Doorsdb.Count;
            for (int i = 0; i < count; i++)
            {
                Exiled.API.Features.Door doorVariant = ScpLockdown.Instance.Handler.Doorsdb[i];
                doorVariant.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
                doorVariant.Base.NetworkTargetState = true;
            }
        }

        public static IEnumerator<float> CassieMsg()
        {
            yield return Timing.WaitForSeconds(ScpLockdown.Instance.Config.CassieTime);
            Cassie.Message(ScpLockdown.Instance.Config.CassieMsg);
        }
    }
}
