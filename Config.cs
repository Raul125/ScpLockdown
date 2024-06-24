using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;
using ScpLockdown.API.Features;

namespace ScpLockdown;

public class Config : IConfig
{
    [Description(
        "The affected SCPs, the shown text when unlocked and the time of their lockdown. (RoleType, string, int => RoleType, text, time in seconds)")]
    public List<ScpLocker> AffectedScps { get; set; } =
    [
        new ScpLocker(RoleTypeId.Scp079, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp173, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp096, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp106, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp049, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp939, "Containment Breach!", 60)
    ];

    [Description(
        "Doors that you want to open/unlock/destroy/unlock after x seconds, this doors are locked at round start. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)")]
    public List<AffectedDoor> AffectedDoors { get; set; } =
    [
        new AffectedDoor(DoorType.CheckpointLczA, 60, true, false, false),
        new AffectedDoor(DoorType.CheckpointLczB, 60, true, false, false),
        new AffectedDoor(DoorType.PrisonDoor, 60, false, true, false)
    ];

    [Description(
        "Use this if you want send cassies with a specified timing. (string, int => cassie text, delay in seconds)")]
    public List<CassieAnnouncement> Cassies { get; set; } =
    [
        new CassieAnnouncement(
            "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols",
            60),
        new CassieAnnouncement(
            "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols",
            120)
    ];

    [Description("Can the Scp-079 use/switch cameras while in lockdown?")]
    public bool Scp079Camera { get; set; } = true;

    [Description("the plugin should use hints or broadcasts?.")]
    public bool UseHints { get; set; } = true;

    public bool IsEnabled { get; set; } = true;

    public bool Debug { get; set; } = false;
}