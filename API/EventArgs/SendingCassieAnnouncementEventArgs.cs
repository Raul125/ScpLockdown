namespace SCPLockdown.API.EventArgs
{
    using System;

    public class SendingCassieAnnouncementEventArgs : EventArgs
    {
        public bool IsAllowed { get; set; } = true;
        public string Message { get; }
        public int Delay { get; }

        public SendingCassieAnnouncementEventArgs(string message, int delay)
        {
            Message = message;
            Delay = delay;
        }
    }
}