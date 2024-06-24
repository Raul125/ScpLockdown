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

    public RoleTypeId RoleType { get; init; }
    public string Text { get; init; }
    public int TimeToUnlock { get; init; }
}