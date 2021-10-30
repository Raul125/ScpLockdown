using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using SCPLockdown.API.Features;
using SCPLockdown.API.EventArgs;

namespace SCPLockdown
{
    public static class Methods
    {
        public static void SendCassies()
        {
            foreach (var cassie in SCPLockdown.Instance.Config.Cassies)
            {
                EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(SendCassie(cassie)));
            }
        }

        public static IEnumerator<float> SendCassie(CassieAnnouncement cassie)
        {
            yield return Timing.WaitForSeconds(cassie.Delay);
            var ev = new SendingCassieAnnouncementEventArgs(cassie.Content, cassie.Delay);

            if (ev.IsAllowed)
            {
                Cassie.Message(cassie.Content, false, false);
            }
        }

        public static void LockAffectedDoors()
        {
            foreach (var affectedDoors in SCPLockdown.Instance.Config.AffectedDoors)
            {
                foreach (var door in affectedDoors.Doors)
                {
                    door.ChangeLock(DoorLockType.SpecialDoorFeature);
                }
            }
        }

        public static void ProcessDoors()
        {
            foreach (var door in SCPLockdown.Instance.Config.AffectedDoors)
            {
                EventHandlers.RunningCoroutines.Add(Timing.RunCoroutine(ProcessDoor(door)));
            }
        }

        private static IEnumerator<float> ProcessDoor(AffectedDoor affectedDoor)
        {
            yield return Timing.WaitForSeconds(affectedDoor.Delay);
            var ev = new ProcessingAffectedDoorEventArgs(affectedDoor.Doors, affectedDoor.Delay, affectedDoor.Unlock, affectedDoor.Open, affectedDoor.Destroy);

            if (ev.IsAllowed)
            {
                if (affectedDoor.Destroy)
                {
                    foreach (var door in affectedDoor.Doors)
                    {
                        door.BreakDoor();
                    }

                    yield break;
                }

                if (affectedDoor.Unlock)
                {
                    foreach (var door in affectedDoor.Doors)
                    {
                        door.Unlock();
                    }
                }

                if (affectedDoor.Open)
                {
                    foreach (var door in affectedDoor.Doors)
                    {
                        door.IsOpen = true;
                    }
                }
            }
        }
    }
}
