namespace ScpLockdown.API.Features;

using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using YamlDotNet.Serialization;

public class AffectedDoor
{
    public AffectedDoor()
    {

    }

    public AffectedDoor(DoorType doorType, int delay, bool unlock, bool open, bool destroy)
    {
        DoorType = doorType;
        Delay = delay;
        Unlock = unlock;
        Open = open;
        Destroy = destroy;
    }

    public DoorType DoorType { get; set; }
    public int Delay { get; set; }
    public bool Unlock { get; set; }
    public bool Destroy { get; set; }
    public bool Open { get; set; }

    [YamlIgnore]

    public readonly List<Door> Doors = [];
}
