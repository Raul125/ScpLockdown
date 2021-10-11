using Exiled.API.Interfaces;
using Exiled.API.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace ScpLockdown
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("The affected SCPs and their duration [seconds] of lockdown.")]
        public Dictionary<RoleType, int> AffectedScps { get; set; } = new Dictionary<RoleType, int>()
        {
            { RoleType.Scp079, 60 },
            { RoleType.Scp173, 60 },
            { RoleType.Scp096, 60 },
            { RoleType.Scp106, 60 },
            { RoleType.Scp049, 60 },
            { RoleType.Scp93989, 60 },
            { RoleType.Scp93953, 60 }
        };

        [Description("Can the Scp-079 use/switch cameras while is in lockdown?")]
        public bool Scp079Camera { get; private set; } = true;

        [Description("Use this if you want to lock any doors, if you enabled a lockdown of an scp in AffectedScps cfg you don't need to enable their doors lockdown here, Use PrisonDoor to lock class-d cells.")]
        public Dictionary<DoorType, int> AffectedDoors { get; set; } = new Dictionary<DoorType, int>()
        {
            { DoorType.CheckpointLczA, 60 },
            { DoorType.CheckpointLczB, 60 },
            { DoorType.PrisonDoor, 60 }
        };

        [Description("Use this if you want send cassies with a specified timing.")]
        public Dictionary<string, int> Cassies { get; set; } = new Dictionary<string, int>()
        {
            { "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols", 60 },
            { "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols", 120 }
        };

        [Description("If enabled, the scps will see a hint, else they will see a broadcast.")]
        public bool UseHints = true;

        [Description("Displayed to the scps when his lockdown is finished.")]
        public Dictionary<RoleType, string> ScpsText { get; set; } = new Dictionary<RoleType, string>()
        {
            { RoleType.Scp079, "Containment Breach!" },
            { RoleType.Scp173, "Containment Breach!" },
            { RoleType.Scp096, "Containment Breach!" },
            { RoleType.Scp106, "Containment Breach!" },
            { RoleType.Scp049, "Containment Breach!" },
            { RoleType.Scp93989, "Containment Breach!" },
            { RoleType.Scp93953, "Containment Breach!" }
        };

        [YamlIgnore]
        public Dictionary<RoleType, int> CheckedAffectedScps { get; set; } = new Dictionary<RoleType, int>();

        [YamlIgnore]
        public Dictionary<DoorType, int> CheckedAffectedDoors { get; set; } = new Dictionary<DoorType, int>();

        public void PreventDuplicatedCfgs()
        {
            foreach (var entry in AffectedScps)
            {
                if (!CheckedAffectedScps.ContainsKey(entry.Key))
                {
                    CheckedAffectedScps.Add(entry.Key, entry.Value);
                }
            }

            foreach (var entry in AffectedDoors)
            {
                if (!CheckedAffectedDoors.ContainsKey(entry.Key))
                {
                    CheckedAffectedDoors.Add(entry.Key, entry.Value);
                }
            }
        }
    }
}