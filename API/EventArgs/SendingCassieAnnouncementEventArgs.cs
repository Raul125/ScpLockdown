namespace ScpLockdown.API.EventArgs;

using Exiled.Events.EventArgs.Interfaces;
using System;

public class SendingCassieAnnouncementEventArgs(string message, int delay) : EventArgs, IExiledEvent
{
    public bool IsAllowed { get; set; } = true;
    public string Message { get; } = message;
    public int Delay { get; } = delay;
}