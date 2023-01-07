namespace SCPLockdown.API.EventArgs
{
    using Exiled.Events.EventArgs.Interfaces;
    using System;

    public class SendingCassieAnnouncementEventArgs : EventArgs, IExiledEvent
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