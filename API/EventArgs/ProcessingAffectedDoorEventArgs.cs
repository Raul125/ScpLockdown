namespace SCPLockdown.API.EventArgs
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Interfaces;
    using System;
    using System.Collections.Generic;

    public class ProcessingAffectedDoorEventArgs : EventArgs, IExiledEvent
    {
        public bool IsAllowed { get; set; } = true;
        public List<Door> Doors { get; }
        public int Delay { get; set; }
        public bool Unlock { get; set; }
        public bool Open { get; set; }
        public bool Destroy { get; set; }

        public ProcessingAffectedDoorEventArgs(List<Door> doors, int delay, bool unlock, bool open, bool destroy)
        {
            Doors = doors;
            Delay = delay;
            Unlock = unlock;
            Open = open;
            Destroy = destroy;
        }
    }
}