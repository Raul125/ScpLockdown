namespace SCPLockdown.API.Extensions;

using Exiled.API.Features;
using Features;
using PlayerRoles;
using UnityEngine;

public static class Extensions
{
    private static readonly Vector3 pocketDimensionPosition = new(0, -1999f, 0);
    public static void SendToPocketDimension(this Player player) => player.Position = pocketDimensionPosition;

    public static bool LockedUpState(this RoleTypeId role)
    {
        return role switch
        {
            RoleTypeId.Scp079 => LockdownController.IsScp079LockedUp,
            RoleTypeId.Scp096 => LockdownController.IsScp096LockedUp,
            RoleTypeId.Scp106 => LockdownController.IsScp106LockedUp,
            RoleTypeId.Scp049 => LockdownController.IsScp049LockedUp,
            RoleTypeId.Scp173 => LockdownController.IsScp173LockedUp,
            RoleTypeId.Scp939 => LockdownController.IsScp939LockedUp,
            _ => false,
        };
    }

    public static void SendContainmentBreachText(this Player player)
    {
        if (!player.IsScp)
            return;

        string text = SCPLockdown.Instance.Config.AffectedScps.Find(x => x.RoleType == player.Role).Text;
        if (SCPLockdown.Instance.Config.UseHints)
            player.ShowHint(text, 10);
        else
            player.Broadcast(10, text);
    }
}