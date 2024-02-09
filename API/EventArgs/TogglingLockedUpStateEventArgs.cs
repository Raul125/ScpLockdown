namespace ScpLockdown.API.EventArgs;

using Exiled.Events.EventArgs.Interfaces;
using PlayerRoles;
using System;

public class TogglingLockedUpStateEventArgs(RoleTypeId role, bool before, bool after) : EventArgs, IExiledEvent
{
    public bool IsAllowed { get; set; } = true;
    public RoleTypeId Scp { get; } = role;
    public bool StateBefore { get; } = before;
    public bool StateAfter { get; } = after;
}