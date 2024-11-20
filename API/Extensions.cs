using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using ScpLockdown.API.Features;
using UnityEngine;

namespace ScpLockdown.API;

public static class Extensions
{
    public static bool LockedUpState(this RoleTypeId role)
    {
        return LockdownController.ScpLockStates.TryGetValue(role, out var isLockedUp) && isLockedUp;
    }

    public static void SendContainmentBreachText(this Player player)
    {
        if (!player.IsScp)
            return;

        var text = ScpLockdown.Instance.Config.AffectedScps.Find(x => x.RoleType == player.Role).Text;
        if (ScpLockdown.Instance.Config.UseHints)
            player.ShowHint(text, 10);
        else
            player.Broadcast(10, text, shouldClearPrevious: ScpLockdown.Instance.Config.ClearBroadcasts);
    }
}