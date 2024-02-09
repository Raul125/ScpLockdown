namespace ScpLockdown.API.Extensions;

using Exiled.API.Features;
using Features;
using PlayerRoles;
using UnityEngine;

public static class Extensions
{
    public static readonly Vector3 PocketDimensionPosition = new(0, -1999f, 0);
    public static void SendToPocketDimension(this Player player) => player.Position = PocketDimensionPosition;

    public static bool LockedUpState(this RoleTypeId role)
    {
        return LockdownController.ScpLockStates.TryGetValue(role, out bool isLockedUp) && isLockedUp;
    }

    public static void SendContainmentBreachText(this Player player)
    {
        if (!player.IsScp)
            return;

        string text = ScpLockdown.Instance.Config.AffectedScps.Find(x => x.RoleType == player.Role).Text;
        if (ScpLockdown.Instance.Config.UseHints)
            player.ShowHint(text, 10);
        else
            player.Broadcast(10, text);
    }
}