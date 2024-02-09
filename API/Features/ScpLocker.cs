namespace ScpLockdown.API.Features;

using PlayerRoles;

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

    public RoleTypeId RoleType { get; set; }
    public string Text { get; set; }
    public int TimeToUnlock { get; set; }
}
