using Exiled.Events.Features;
using ScpLockdown.API.EventArgs;

namespace ScpLockdown.API.Events;

public static class Handlers
{
    public static readonly Event<ProcessingAffectedDoorEventArgs> ProcessingAffectedDoor = new();

    public static readonly Event<SendingCassieAnnouncementEventArgs> SendingCassieAnnouncements = new();

    public static readonly Event<TogglingLockedUpStateEventArgs> TogglingLockedUpState = new();

    public static void OnProcessingAffectedDoor(ProcessingAffectedDoorEventArgs ev)
    {
        ProcessingAffectedDoor.InvokeSafely(ev);
    }

    public static void OnSendingCassieAnnouncements(SendingCassieAnnouncementEventArgs ev)
    {
        SendingCassieAnnouncements.InvokeSafely(ev);
    }

    public static void OnChangingLockedUpStatus(TogglingLockedUpStateEventArgs ev)
    {
        TogglingLockedUpState.InvokeSafely(ev);
    }
}