using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;
using ScpLockdown.API.EventArgs;
using ScpLockdown.API.Features;

namespace ScpLockdown;

public static class Methods
{
    public static void SendCassies()
    {
        foreach (CassieAnnouncement cassie in ScpLockdown.Instance.Config.Cassies)
        {
            ScpLockdown.RunningCoroutines.Add(Timing.RunCoroutine(SendCassie(cassie)));
        }
    }

    private static IEnumerator<float> SendCassie(CassieAnnouncement cassie)
    {
        yield return Timing.WaitForSeconds(cassie.Delay);
        SendingCassieAnnouncementEventArgs ev = new(cassie.Content, cassie.Delay);

        if (ev.IsAllowed)
        {
            if (cassie.Subtitle != string.Empty)
            {
                Cassie.MessageTranslated(cassie.Content, cassie.Subtitle, false, false);
            }
            else
            {
                Cassie.Message(cassie.Content, false, false);
            }
        }
    }

    public static void LockAffectedDoors()
    {
        foreach (Door door in ScpLockdown.Instance.Config.AffectedDoors.SelectMany(affectedDoors => affectedDoors.Doors))
        {
            door.ChangeLock(DoorLockType.SpecialDoorFeature);
        }
    }

    public static void ProcessDoors()
    {
        foreach (AffectedDoor door in ScpLockdown.Instance.Config.AffectedDoors)
        {
            ScpLockdown.RunningCoroutines.Add(Timing.RunCoroutine(ProcessDoor(door)));
        }
    }

    private static IEnumerator<float> ProcessDoor(AffectedDoor affectedDoor)
    {
        yield return Timing.WaitForSeconds(affectedDoor.Delay);
        ProcessingAffectedDoorEventArgs ev = new(affectedDoor.Doors, affectedDoor.Delay, affectedDoor.Unlock,
                                                     affectedDoor.Open, affectedDoor.Destroy);

        if (!ev.IsAllowed)
        {
            yield break;
        }

        if (ev.Destroy)
        {
            foreach (Door door in affectedDoor.Doors)
            {
                if (door is BreakableDoor breakableDoor)
                {
                    breakableDoor.Break();
                }
            }

            yield break;
        }

        if (ev.Unlock)
        {
            foreach (Door door in affectedDoor.Doors)
            {
                door.Unlock();
            }
        }

        if (!ev.Open)
        {
            yield break;
        }

        foreach (Door door in affectedDoor.Doors)
        {
            door.IsOpen = true;
        }
    }
}