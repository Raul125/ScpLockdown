namespace SCPLockdown.API.EventArgs;

using Exiled.Events.EventArgs.Interfaces;
using PlayerRoles;
using System;

public class TogglingLockedUpStateEventArgs : EventArgs, IExiledEvent
{
    public TogglingLockedUpStateEventArgs(RoleTypeId role, bool before, bool after)
    {
        Scp = role;
        StateBefore = before;
        StateAfter = after;
    }

    public bool IsAllowed { get; set; } = true;
    public RoleTypeId Scp { get; }
    public bool StateBefore { get; }
    public bool StateAfter { get; }
}