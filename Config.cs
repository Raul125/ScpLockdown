﻿namespace ScpLockdown;

using Exiled.API.Interfaces;
using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using API.Features;
using PlayerRoles;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    
    public bool Debug { get; set; } = false;

    [Description("The affected SCPs, their shown text when unlocked and the time of their lockdown. (RoleType, string, int => RoleType, text, time in seconds)")]
    public List<ScpLocker> AffectedScps { get; private set; } =
    [
        new(RoleTypeId.Scp079, "Containment Breach!", 60),
        new(RoleTypeId.Scp173, "Containment Breach!", 60),
        new(RoleTypeId.Scp096, "Containment Breach!", 60),
        new(RoleTypeId.Scp106, "Containment Breach!", 60),
        new(RoleTypeId.Scp049, "Containment Breach!", 60),
        new(RoleTypeId.Scp939, "Containment Breach!", 60)
    ];

    [Description("Doors that you want to open/unlock/destroy/unlock after x seconds, this doors are locked at the round start. (DoorType, int, bool, bool, bool => DoorType, delay in seconds, unlock?, open?, destroy?)")]
    public List<AffectedDoor> AffectedDoors { get; private set; } =
    [
        new(DoorType.CheckpointLczA, 60, true, false, false),
        new(DoorType.CheckpointLczB, 60, true, false, false),
        new(DoorType.PrisonDoor, 60, false, true, false)
    ];

    [Description("Use this if you want send cassies with a specified timing. (string, int => cassie text, delay in seconds)")]
    public List<CassieAnnouncement> Cassies { get; private set; } =
    [
        new("containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols", 60),
        new("containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols", 120)
    ];

    [Description("Can the Scp-079 use/switch cameras while is in lockdown?")]
    public bool Scp079Camera { get; private set; } = true;

    [Description("the plugin should use hints or broadcasts?.")]
    public bool UseHints { get; private set; } = true;
}
