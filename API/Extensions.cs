namespace SCPLockdown.API.Extensions
{
    using Exiled.API.Features;
    using Features;
    using PlayerRoles;
    using UnityEngine;

    public static class Extensions
    {
        private static readonly Vector3 pocketDimensionPosition = new Vector3(0, -1999f, 0);
        public static void SendToPocketDimension(this Player player) => player.Position = pocketDimensionPosition;

        public static bool LockedUpState(this RoleTypeId role)
        {
            switch (role)
            {
                case RoleTypeId.Scp079:
                    return LockdownController.IsScp079LockedUp;
                case RoleTypeId.Scp096:
                    return LockdownController.IsScp096LockedUp;
                case RoleTypeId.Scp106:
                    return LockdownController.IsScp106LockedUp;
                case RoleTypeId.Scp049:
                    return LockdownController.IsScp049LockedUp;
                case RoleTypeId.Scp173:
                    return LockdownController.IsScp173LockedUp;
                case RoleTypeId.Scp939:
                    return LockdownController.IsScp939LockedUp;
            }

            return false;
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
}
