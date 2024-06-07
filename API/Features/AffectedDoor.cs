using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using YamlDotNet.Serialization;

namespace ScpLockdown.API.Features;

public class AffectedDoor
{
    [YamlIgnore] 
    public readonly List<Door> Doors = [];

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

    public DoorType DoorType { get; }
    public int Delay { get; }
    public bool Unlock { get; }
    public bool Destroy { get; }
    public bool Open { get; }
}