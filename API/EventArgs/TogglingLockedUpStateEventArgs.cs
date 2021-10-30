namespace SCPLockdown.API.EventArgs
{
    using System;

    public class TogglingLockedUpStateEventArgs : EventArgs
    {
        public bool IsAllowed { get; set; } = true;
        public RoleType Scp { get; }
        public bool StateBefore { get; }
        public bool StateAfter { get; }

        public TogglingLockedUpStateEventArgs(RoleType role, bool before, bool after)
        {
            Scp = role;
            StateBefore = before;
            StateAfter = after;
        }
    }
}