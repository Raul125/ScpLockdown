using Exiled.API.Enums;
using Exiled.API.Features;
using Interactables.Interobjects.DoorUtils;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ScpLockdown.Helper
{
    public class Methods
    {
        private ScpLockdown scpLockdown;

        public Methods(ScpLockdown scpLockdown)
        {
            this.scpLockdown = scpLockdown;
        }

        public Task Lockdown106(KeyValuePair<RoleType, int> scp)
        {
            foreach (var player in Player.List.Where((e) => e.Role == RoleType.Scp106))
            {
                var prevPos = player.Position;
                player.SendToPocketDimension();
                player.IsGodModeEnabled = true;

                scpLockdown._lockdownHandler.larryCoroutines.Add(new KeyValuePair<Player, CoroutineHandle>(player, Timing.CallDelayed(scp.Value, () =>
                {
                    player.Position = prevPos;
                    player.IsGodModeEnabled = false;
                    scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp106);

                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                })));
            }
            return Task.CompletedTask;
        }

        public Task Lockdown079(KeyValuePair<RoleType, int> scp)
        {
            scpLockdown._lockdownHandler.runningCoroutines.Add(Timing.CallDelayed(scp.Value, () =>
            {
                scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp079);
                foreach (var player in Player.List.Where((player) => player.Role == RoleType.Scp079))
                {
                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                }
            }));
            return Task.CompletedTask;
        }

        public Task Lockdown049(KeyValuePair<RoleType, int> scp)
        {
            var door049 = DoorNametagExtension.NamedDoors["049_ARMORY"];
            var heavyDoor049 = Map.Doors.GetClosestDoor(door049.TargetDoor);
            heavyDoor049.ServerChangeLock(DoorLockReason.AdminCommand, true);
            scpLockdown._lockdownHandler.runningCoroutines.Add(Timing.CallDelayed(scp.Value, () =>
            {
                heavyDoor049.ServerChangeLock(DoorLockReason.AdminCommand, false);
                scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp049);

                foreach (var player in Player.List.Where((player) => player.Role == RoleType.Scp049))
                {
                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                }
            }));
            return Task.CompletedTask;
        }

        public Task Lockdown096(KeyValuePair<RoleType, int> scp)
        {
            var door096 = DoorNametagExtension.NamedDoors["096"];
            DoorVariant nearestDoor = Map.Doors.GetClosestDoor(door096.TargetDoor);
            nearestDoor.ServerChangeLock(DoorLockReason.AdminCommand, true);
            scpLockdown._lockdownHandler.runningCoroutines.Add(Timing.CallDelayed(scp.Value, () =>
            {
                nearestDoor.ServerChangeLock(DoorLockReason.AdminCommand, false);
                scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp096);

                foreach (var player in Player.List.Where((player) => player.Role == RoleType.Scp096))
                {
                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                }
            }));
            return Task.CompletedTask;
        }

        public Task Lockdown173(KeyValuePair<RoleType, int> scp)
        {
            DoorVariant door173 = Map.GetDoorByName("173_CONNECTOR");
            door173.TargetState = false;
            door173.ServerChangeLock(DoorLockReason.AdminCommand, true);

            scpLockdown._lockdownHandler.runningCoroutines.Add(Timing.CallDelayed(scp.Value, () =>
            {
                door173.ServerChangeLock(DoorLockReason.AdminCommand, false);
                scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp173);

                foreach (var player in Player.List.Where((player) => player.Role == RoleType.Scp173))
                {
                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                }
            }));
            return Task.CompletedTask;
        }

        public Task Lockdown939(KeyValuePair<RoleType, int> scp)
        {
            List<DoorVariant> targetDoors = new List<DoorVariant>();

            Room room939 = Map.Rooms.First(x => x.Type == RoomType.Hcz939);

            targetDoors.Add(Map.Doors.GetClosestDoor(room939));
            targetDoors.Add(Map.Doors.GetClosestDoor(room939, false, targetDoors));

            foreach (var door in targetDoors)
            {
                door.ServerChangeLock(DoorLockReason.AdminCommand, true);
            }

            scpLockdown._lockdownHandler.runningCoroutines.Add(Timing.CallDelayed(scp.Value, () =>
            {
                foreach (var door in targetDoors)
                {
                    door.ServerChangeLock(DoorLockReason.AdminCommand, false);
                }

                scpLockdown._lockdownHandler._lockdownStates.ToggleLockedUpState(RoleType.Scp93953);

                foreach (var player in Player.List.Where((player) => player.Role == RoleType.Scp93953 || player.Role == RoleType.Scp93989))
                {
                    player.ShowHint(scpLockdown.Config.CBHint, 5);
                }
            }));
            return Task.CompletedTask;
        }

        public IEnumerator<float> OpenDoorsAfterTime()
        {
            yield return Timing.WaitForSeconds(scpLockdown.Config.ClassDLock);
            OpenClassDDoors();
            yield break;
        }

        public void OpenClassDDoors()
        {
            int count = scpLockdown._lockdownHandler.doorsdb.Count;
            for (int i = 0; i < count; i++)
            {
                DoorVariant doorVariant = scpLockdown._lockdownHandler.doorsdb[i];
                doorVariant.ServerChangeLock(DoorLockReason.Regular079, false);
                doorVariant.NetworkTargetState = true;
            }
        }

        public IEnumerator<float> CassieMsg()
        {
            yield return Timing.WaitForSeconds(scpLockdown.Config.CassieTime);
            Cassie.Message(scpLockdown.Config.CassieMsg);
            yield break;
        }
    }
}
