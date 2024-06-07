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
        foreach (var cassie in ScpLockdown.Instance.Config.Cassies)
            ScpLockdown.RunningCoroutines.Add(Timing.RunCoroutine(SendCassie(cassie)));
    }

    private static IEnumerator<float> SendCassie(CassieAnnouncement cassie)
    {
        yield return Timing.WaitForSeconds(cassie.Delay);
        var ev = new SendingCassieAnnouncementEventArgs(cassie.Content, cassie.Delay);

        if (ev.IsAllowed)
            Cassie.Message(cassie.Content, false, false);
    }

    public static void LockAffectedDoors()
    {
        foreach (var door in ScpLockdown.Instance.Config.AffectedDoors.SelectMany(affectedDoors => affectedDoors.Doors))
            door.ChangeLock(DoorLockType.SpecialDoorFeature);
    }

    public static void ProcessDoors()
    {
        foreach (var door in ScpLockdown.Instance.Config.AffectedDoors)
            ScpLockdown.RunningCoroutines.Add(Timing.RunCoroutine(ProcessDoor(door)));
    }

    private static IEnumerator<float> ProcessDoor(AffectedDoor affectedDoor)
    {
        yield return Timing.WaitForSeconds(affectedDoor.Delay);
        var ev = new ProcessingAffectedDoorEventArgs(affectedDoor.Doors, affectedDoor.Delay, affectedDoor.Unlock,
            affectedDoor.Open, affectedDoor.Destroy);

        if (!ev.IsAllowed)
            yield break;

        if (ev.Destroy)
        {
            foreach (var door in affectedDoor.Doors)
                if (door is BreakableDoor breakableDoor)
                    breakableDoor.Break();

            yield break;
        }

        if (ev.Unlock)
            foreach (var door in affectedDoor.Doors)
                door.Unlock();

        if (!ev.Open) 
            yield break;
        
        foreach (var door in affectedDoor.Doors)
            door.IsOpen = true;
    }
}