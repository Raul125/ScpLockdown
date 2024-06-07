using Exiled.API.Features;
using PlayerRoles;
using ScpLockdown.API.Features;
using UnityEngine;

namespace ScpLockdown.API;

public static class Extensions
{
    public static readonly Vector3 PocketDimensionPosition = new(0, -1999f, 0);

    public static void SendToPocketDimension(this Player player)
    {
        player.Position = PocketDimensionPosition;
    }

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
            player.Broadcast(10, text);
    }
}