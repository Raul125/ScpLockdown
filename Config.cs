using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;
using ScpLockdown.API.Features;
using System.ComponentModel;

namespace ScpLockdown;

public class Config : IConfig
{
    [Description("Defines the affected SCPs, the text displayed upon unlocking, and the lockdown duration. (RoleTypeId, string, int => RoleType, display text, lockdown time in seconds)")]
    public List<ScpLocker> AffectedScps { get; set; } =
    [
        new ScpLocker(RoleTypeId.Scp079, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp173, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp096, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp106, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp049, "Containment Breach!", 60),
        new ScpLocker(RoleTypeId.Scp939, "Containment Breach!", 60)
    ];

    [Description("Specifies doors to manage (e.g., lock, unlock, open, or destroy) after a delay. These doors are locked at the start of the round. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)")]
    public List<AffectedDoor> AffectedDoors { get; set; } =
    [
        new AffectedDoor(DoorType.CheckpointLczA, 60, true, false, false),
        new AffectedDoor(DoorType.CheckpointLczB, 60, true, false, false),
        new AffectedDoor(DoorType.PrisonDoor, 60, false, true, false)
    ];

    [Description("Defines CASSIE announcements with specified timing. (string, string, int => CASSIE message text, subtitle text [optional, leave it empty ''], delay in seconds)")]
    public List<CassieAnnouncement> Cassies { get; set; } =
    [
        new CassieAnnouncement(
            "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols",
            "Containment breach detected!",
            60),
        new CassieAnnouncement(
            "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols",
            "Containment breach detected!",
            120)
    ];

    [Description("Determines if SCP-079 can use or switch cameras while in lockdown.")]
    public bool Scp079Camera { get; set; } = true;

    [Description("Specifies whether the plugin should display hints or broadcasts.")]
    public bool UseHints { get; set; } = true;

    [Description("Determines if the plugin should clear previous broadcasts before displaying new ones.")]
    public bool ClearBroadcasts { get; set; } = true;

    [Description("Enables or disables the plugin.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Enables or disables debug mode for the plugin.")]
    public bool Debug { get; set; } = false;
}