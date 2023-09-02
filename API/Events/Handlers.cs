namespace SCPLockdown.API.Events
{
    using Exiled.Events.Features;
    using EventArgs;

    public static class Handlers
    {
        public static Event<ProcessingAffectedDoorEventArgs> ProcessingAffectedDoor = new Event<ProcessingAffectedDoorEventArgs>();
        public static void OnProcessingAffectedDoor(ProcessingAffectedDoorEventArgs ev) => ProcessingAffectedDoor.InvokeSafely(ev);

        public static Event<SendingCassieAnnouncementEventArgs> SendingCassieAnnouncements = new Event<SendingCassieAnnouncementEventArgs>();
        public static void OnSendingCassieAnnouncements(SendingCassieAnnouncementEventArgs ev) => SendingCassieAnnouncements.InvokeSafely(ev);

        public static Event<TogglingLockedUpStateEventArgs> TogglingLockedUpState = new Event<TogglingLockedUpStateEventArgs>();
        public static void OnChangingLockedUpStatus(TogglingLockedUpStateEventArgs ev) => TogglingLockedUpState.InvokeSafely(ev);
    }
}