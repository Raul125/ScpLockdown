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
            foreach (var player in Player.List.Where(e => e.Role == RoleType.Scp106))
            {
                player.SendToPocketDimension();
                player.IsGodModeEnabled = true;
            }
            plugin.EventHandlers.runningCoroutines.Add(Timing.RunCoroutine(Unlock106s(scp.Value)));
        }

        public void LockSingle106(Player ply)
        {
            ply.IsGodModeEnabled = true;
            ply.SendToPocketDimension();
        }

        public IEnumerator<float> Unlock106s(int time)
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

        public IEnumerator<float> Unlock079s(int time)
        {
            yield return Timing.WaitForSeconds(time);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp079))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp079);
        }

        public void Lockdown049(KeyValuePair<RoleType, int> scp)
        {
            var door049 = Map.GetDoorByName("049_ARMORY");
            var heavyDoor049 = Map.Doors.GetClosestDoor(door049);
            heavyDoor049.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock049(scp.Value, heavyDoor049)));
        }

        public IEnumerator<float> Unlock049(int time, Exiled.API.Features.Door door)
        {
            yield return Timing.WaitForSeconds(time);
            door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp049);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp049))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public void Lockdown096(KeyValuePair<RoleType, int> scp)
        {
            var door096 = Map.GetDoorByName("096");
            Exiled.API.Features.Door nearestDoor = Map.Doors.GetClosestDoor(door096);
            nearestDoor.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock096(scp.Value, nearestDoor)));
        }

        public IEnumerator<float> Unlock096(int time, Exiled.API.Features.Door door)
        {
            yield return Timing.WaitForSeconds(time);
            door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp096);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp096))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public void Lockdown173(KeyValuePair<RoleType, int> scp)
        {
            // Using this door because the gate is opened by the base game
            Exiled.API.Features.Door door173 = Map.GetDoorByName("173_CONNECTOR");
            door173.Base.TargetState = false;
            door173.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock096(scp.Value, door173)));
        }

        public IEnumerator<float> Unlock173(int time, DoorVariant door)
        {
            yield return Timing.WaitForSeconds(time);
            door.ServerChangeLock(DoorLockReason.AdminCommand, false);
            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp173);
            foreach (var player in Player.List.Where(x => x.Role == RoleType.Scp173))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public void Lockdown939(KeyValuePair<RoleType, int> scp)
        {
            List<Door> targetDoors = new List<Door>();
            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);
            targetDoors.Add(Map.Doors.GetClosestDoor(room939));
            targetDoors.Add(Map.Doors.GetClosestDoor(room939, false, targetDoors));
            foreach (var door in targetDoors)
            {
                door.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
            }

            ScpLockdown.Instance.Handler.runningCoroutines.Add(Timing.RunCoroutine(Unlock939(scp.Value, targetDoors)));
        }

        public IEnumerator<float> Unlock939(int time, List<Door> doors)
        {
            yield return Timing.WaitForSeconds(time);
            foreach (var door in doors)
            {
                door.Unlock();
            }

            ScpLockdown.Instance.Handler._lockdownStates.ToggleLockedUpState(RoleType.Scp93953);
            foreach (var player in Player.List.Where(x => x.Role.Is939()))
            {
                player.ShowHint(ScpLockdown.Instance.Config.CBHint, 5);
            }
        }

        public IEnumerator<float> OpenDoorsAfterTime()
        {
            yield return Timing.WaitForSeconds(ScpLockdown.Instance.Config.ClassDLock);
            foreach (var door in Handler.ClassDDoors)
            {
                door.Unlock();
                door.IsOpen = true;
            }
        }

        public void SendCassies()
        {

        }
    }
}
