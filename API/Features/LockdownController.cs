﻿using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;
using PlayerRoles;
using ScpLockdown.API.EventArgs;
using UnityEngine;

namespace ScpLockdown.API.Features;

public static class LockdownController
{
    public static readonly Dictionary<RoleTypeId, bool> ScpLockStates = new()
    {
        { RoleTypeId.Scp079, false },
        { RoleTypeId.Scp096, false },
        { RoleTypeId.Scp106, false },
        { RoleTypeId.Scp049, false },
        { RoleTypeId.Scp939, false },
        { RoleTypeId.Scp173, false }
    };

    public static readonly Dictionary<RoleTypeId, IEnumerable<Door>> ScpDoors = [];

    public static void ToggleLockedUpState(RoleTypeId role)
    {
        if (ScpLockStates.ContainsKey(role))
        {
            ScpLockStates[role] = !ScpLockStates[role];
        }
    }

    public static void ResetAllStates()
    {
        foreach (RoleTypeId key in ScpLockStates.Keys.ToList())
        {
            ScpLockStates[key] = false;
        }
    }

    public static void LockdownScp(RoleTypeId role, int time)
    {
        ToggleLockedUpState(role);
        if (role == RoleTypeId.Scp106)
        {
            foreach (Player ply in Player.Get(role))
            {
                if (ply.CurrentRoom.Type != RoomType.Pocket)
                {
                    ply.Teleport(RoomType.Pocket);
                }
            }
        }

        ScpLockdown.RunningCoroutines.Add(Timing.RunCoroutine(UnlockScp(role, time)));
        if (!ScpDoors.TryGetValue(role, out IEnumerable<Door> doors))
        {
            return;
        }

        foreach (Door door in doors)
        {
            door.ChangeLock(DoorLockType.SpecialDoorFeature);
        }
    }

    private static IEnumerator<float> UnlockScp(RoleTypeId role, int time)
    {
        yield return Timing.WaitForSeconds(time);

        bool state = role.LockedUpState();
        TogglingLockedUpStateEventArgs ev = new(role, state, !state);
        if (!ev.IsAllowed)
        {
            yield break;
        }

        ToggleLockedUpState(role);
        if (ScpDoors.TryGetValue(role, out IEnumerable<Door> doors))
        {
            foreach (Door door in doors)
            {
                door.Unlock();
            }
        }

        if (role == RoleTypeId.Scp106)
        {
            Vector3 pos = RoleTypeId.Scp106.GetRandomSpawnLocation().Position;
            foreach (Player player in Player.Get(role))
            {
                player.Teleport(pos);
            }
        }

        foreach (Player player in Player.Get(role))
        {
            player.SendContainmentBreachText();
        }
    }
}