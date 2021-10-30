using Exiled.API.Features;
using SCPLockdown.API.Features;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SCPLockdown.API.Extensions
{
    public static class Extensions
    {
        private static readonly Vector3 PocketDimensionPosition = new Vector3(0, -1998.67f, 2);
        public static void SendToPocketDimension(this Player player) => player.Position = PocketDimensionPosition;

        public static bool LockedUpState(this RoleType role)
        {
            switch (role)
            {
                case RoleType.Scp079:
                    return LockdownController.IsScp079LockedUp;
                case RoleType.Scp096:
                    return LockdownController.IsScp096LockedUp;
                case RoleType.Scp106:
                    return LockdownController.IsScp106LockedUp;
                case RoleType.Scp049:
                    return LockdownController.IsScp049LockedUp;
                case RoleType.Scp173:
                    return LockdownController.IsScp173LockedUp;
                case RoleType.Scp93953:
                case RoleType.Scp93989:
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
            {
                player.ShowHint(text, 10);
            }
            else
            {
                player.Broadcast(10, text);
            }
        }

        public static Door GetClosestDoor(this IEnumerable<Door> doors, Door relativeDoor, bool onlyHeavyDoors = false, IEnumerable<Door> ignoreDoors = null)
        {
            ignoreDoors = ignoreDoors ?? Enumerable.Empty<Door>();
            Door result = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = relativeDoor.Base.gameObject.transform.position;
            foreach (Door potentialDoor in doors)
            {
                if (onlyHeavyDoors)
                    continue;
                if (ignoreDoors.Contains(potentialDoor))
                    continue;
                if (potentialDoor == relativeDoor)
                    continue;

                Vector3 directionToTarget = potentialDoor.Base.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    result = potentialDoor;
                }
            }

            return result;
        }

        public static Door GetClosestDoor(this IEnumerable<Door> doors, Room relativeRoom, bool onlyHeavyDoors = false, IEnumerable<Door> ignoreDoors = null)
        {
            ignoreDoors = ignoreDoors ?? Enumerable.Empty<Door>();
            Door result = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = relativeRoom.Transform.position;
            foreach (Door potentialDoor in doors)
            {
                if (onlyHeavyDoors)
                    continue;
                if (ignoreDoors.Contains(potentialDoor))
                    continue;

                Vector3 directionToTarget = potentialDoor.Base.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    result = potentialDoor;
                }
            }

            return result;
        }

        public static Door GetClosestDoor(this IEnumerable<Door> doors, Room relativeRoom, Door ignoreDoor, bool onlyHeavyDoors = false)
        {
            Door result = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = relativeRoom.Transform.position;
            foreach (Door potentialDoor in doors)
            {
                if (onlyHeavyDoors)
                    continue;
                if (ignoreDoor == potentialDoor)
                    continue;

                Vector3 directionToTarget = potentialDoor.Base.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    result = potentialDoor;
                }
            }

            return result;
        }
    }
}
