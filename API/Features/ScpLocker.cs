namespace SCPLockdown.API.Features
{
    public class ScpLocker
    {
        public RoleType RoleType { get; set; }
        public string Text { get; set; }
        public int TimeToUnlock { get; set; }

        public ScpLocker()
        {

        }

        public ScpLocker(RoleType role, string text, int timeToUnlock)
        {
            RoleType = role;
            Text = text;
            TimeToUnlock = timeToUnlock;
        }
    }
}
