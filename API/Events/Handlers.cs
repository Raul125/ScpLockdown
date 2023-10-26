namespace SCPLockdown.API.Events;

using Exiled.Events.Features;
using EventArgs;

public static class Handlers
{
    public static Event<ProcessingAffectedDoorEventArgs> ProcessingAffectedDoor = new();
    public static void OnProcessingAffectedDoor(ProcessingAffectedDoorEventArgs ev) => ProcessingAffectedDoor.InvokeSafely(ev);

    public static Event<SendingCassieAnnouncementEventArgs> SendingCassieAnnouncements = new();
    public static void OnSendingCassieAnnouncements(SendingCassieAnnouncementEventArgs ev) => SendingCassieAnnouncements.InvokeSafely(ev);

    public static Event<TogglingLockedUpStateEventArgs> TogglingLockedUpState = new();
    public static void OnChangingLockedUpStatus(TogglingLockedUpStateEventArgs ev) => TogglingLockedUpState.InvokeSafely(ev);
}