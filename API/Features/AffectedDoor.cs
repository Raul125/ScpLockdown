namespace SCPLockdown.API.Features
{
    using Exiled.API.Enums;
    using Exiled.API.Features.Doors;
    using System.Collections.Generic;
    using YamlDotNet.Serialization;

    public class AffectedDoor
    {
        public DoorType DoorType { get; set; }
        public int Delay { get; set; }
        public bool Unlock { get; set; }
        public bool Destroy { get; set; }
        public bool Open { get; set; }

        [YamlIgnore]
        public List<Door> Doors { get; set; } = new List<Door>();

        public AffectedDoor()
        {

        }

        public AffectedDoor(DoorType doortype, int delay, bool unlock, bool open, bool destroy)
        {
            DoorType = doortype;
            Delay = delay;
            Unlock = unlock;
            Destroy = destroy;
            Open = open;
        }
    }
}
