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

    public DoorType DoorType { get; init; }
    public int Delay { get; init; }
    public bool Unlock { get; init; }
    public bool Destroy { get; init; }
    public bool Open { get; init; }
}