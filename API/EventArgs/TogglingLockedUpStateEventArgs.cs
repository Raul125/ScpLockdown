using Exiled.Events.EventArgs.Interfaces;
using PlayerRoles;

namespace ScpLockdown.API.EventArgs;

public class TogglingLockedUpStateEventArgs(RoleTypeId role, bool before, bool after) : System.EventArgs, IExiledEvent
{
    public bool IsAllowed { get; set; } = true;
    public RoleTypeId Scp { get; } = role;
    public bool StateBefore { get; } = before;
    public bool StateAfter { get; } = after;
}