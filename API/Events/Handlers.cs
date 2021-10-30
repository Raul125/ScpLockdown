using Exiled.Events.Extensions;
using SCPLockdown.API.EventArgs;

namespace SCPLockdown.API.Events
{
    public static class Handlers
    {
        public static event Exiled.Events.Events.CustomEventHandler<ProcessingAffectedDoorEventArgs> ProcessingAffectedDoor;
        public static void OnProcessingAffectedDoor(ProcessingAffectedDoorEventArgs ev) => ProcessingAffectedDoor.InvokeSafely(ev);

        public static event Exiled.Events.Events.CustomEventHandler<SendingCassieAnnouncementEventArgs> SendingCassieAnnouncements;
        public static void OnSendingCassieAnnouncements(SendingCassieAnnouncementEventArgs ev) => SendingCassieAnnouncements.InvokeSafely(ev);

        public static event Exiled.Events.Events.CustomEventHandler<TogglingLockedUpStateEventArgs> TogglingLockedUpState;
        public static void OnChangingLockedUpStatus(TogglingLockedUpStateEventArgs ev) => TogglingLockedUpState.InvokeSafely(ev);
    }
}