using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        [Description("Time of Class-D locked in his cells, 0 is disabled")]
        public int ClassDLock { get; private set; } = 0;

        [Description("Displayed to the scps when his lockdown is finished.")]
        public string CBHint { get; private set; } = "Containment Breach!";

        [Description("Custom Cassie for simulating containment breach announce.")]
        public string CassieMsg { get; private set; } = "containment breach detected All remaining personnel are advised to proceed with standard evacuation protocols";

        [Description("Cassie Time to be played, 0 to disable it")]
        public int CassieTime { get; private set; } = 60;
    }
}