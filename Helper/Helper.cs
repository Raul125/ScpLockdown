using Exiled.API.Features;
using Interactables.Interobjects.DoorUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScpLockdown.Helper
{
    public static class Helper
    {
        public static readonly Vector3 PocketDimensionPosition = new Vector3(0, -1998.67f, 2);

        public static void SendToPocketDimension(this Player player) => player.Position = PocketDimensionPosition;

        public static Exiled.API.Features.Door GetClosestDoor(this IEnumerable<Exiled.API.Features.Door> doors, Exiled.API.Features.Door relativeDoor, bool onlyHeavyDoors = false, IEnumerable<Exiled.API.Features.Door> ignoreDoors = null)
        {
            ignoreDoors = ignoreDoors ?? Enumerable.Empty<Exiled.API.Features.Door>();

            Exiled.API.Features.Door result = null;

            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = relativeDoor.Base.gameObject.transform.position;

            foreach (Exiled.API.Features.Door potentialDoor in doors)
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

        public static Exiled.API.Features.Door GetClosestDoor(this IEnumerable<Exiled.API.Features.Door> doors, Room relativeRoom, bool onlyHeavyDoors = false, IEnumerable<Exiled.API.Features.Door> ignoreDoors = null)
        {
            ignoreDoors = ignoreDoors ?? Enumerable.Empty<Exiled.API.Features.Door>();

            Exiled.API.Features.Door result = null;

            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = relativeRoom.Transform.position;

            foreach (Exiled.API.Features.Door potentialDoor in doors)
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
    }
}
