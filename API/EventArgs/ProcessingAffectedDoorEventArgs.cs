namespace ScpLockdown.API.EventArgs;

using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Interfaces;
using System;
using System.Collections.Generic;

public class ProcessingAffectedDoorEventArgs(List<Door> doors, int delay, bool unlock, bool open, bool destroy) : EventArgs, IExiledEvent
{
    public bool IsAllowed { get; set; } = true;
    public List<Door> Doors { get; } = doors;
    public int Delay { get; set; } = delay;
    public bool Unlock { get; set; } = unlock;
    public bool Open { get; set; } = open;
    public bool Destroy { get; set; } = destroy;
}