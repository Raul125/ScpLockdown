namespace SCPLockdown.API.Events
{
    using Exiled.Events.Extensions;
    using EventArgs;

    public static class Handlers
    {
        public static event Exiled.Events.Events.CustomEventHandler<ProcessingAffectedDoorEventArgs> ProcessingAffectedDoor;
        public static void OnProcessingAffectedDoor(ProcessingAffectedDoorEventArgs ev) => ProcessingAffectedDoor.Invoke(ev);

        public static event Exiled.Events.Events.CustomEventHandler<SendingCassieAnnouncementEventArgs> SendingCassieAnnouncements;
        public static void OnSendingCassieAnnouncements(SendingCassieAnnouncementEventArgs ev) => SendingCassieAnnouncements.Invoke(ev);

        public static event Exiled.Events.Events.CustomEventHandler<TogglingLockedUpStateEventArgs> TogglingLockedUpState;
        public static void OnChangingLockedUpStatus(TogglingLockedUpStateEventArgs ev) => TogglingLockedUpState.Invoke(ev);
    }
}