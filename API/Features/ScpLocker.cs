using PlayerRoles;

namespace ScpLockdown.API.Features;

public class ScpLocker
{
    public ScpLocker()
    {
    }

    public ScpLocker(RoleTypeId role, string text, int timeToUnlock)
    {
        RoleType = role;
        Text = text;
        TimeToUnlock = timeToUnlock;
    }

    public RoleTypeId RoleType { get; }
    public string Text { get; }
    public int TimeToUnlock { get; }
}