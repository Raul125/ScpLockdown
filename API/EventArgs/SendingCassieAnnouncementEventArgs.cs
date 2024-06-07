using Exiled.Events.EventArgs.Interfaces;

namespace ScpLockdown.API.EventArgs;

public class SendingCassieAnnouncementEventArgs(string message, int delay) : System.EventArgs, IExiledEvent
{
    public bool IsAllowed { get; set; } = true;
    public string Message { get; set; } = message;
    public int Delay { get; set; } = delay;
}