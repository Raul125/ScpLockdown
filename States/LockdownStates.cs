namespace ScpLockdown.States
{
    public class LockdownStates
    {
        public static bool Scp079LockedUp { get; private set; }
        public static bool Scp096LockedUp { get; private set; }
        public static bool Scp106LockedUp { get; private set; }
        public static bool Scp049LockedUp { get; private set; }
        public static bool Scp939LockedUp { get; private set; }
        public static bool Scp173LockedUp { get; private set; }
        static LockdownStates()
        {
            Scp079LockedUp = false;
            Scp096LockedUp = false;
            Scp106LockedUp = false;
            Scp049LockedUp = false;
            Scp939LockedUp = false;
            Scp173LockedUp = false;
        }

        public void ToggleLockedUpState(RoleType role)
        {
            switch (role)
            {
                case RoleType.Scp079:
                    Scp079LockedUp = !Scp079LockedUp;
                    break;
                case RoleType.Scp096:
                    Scp096LockedUp = !Scp096LockedUp;
                    break;
                case RoleType.Scp106:
                    Scp106LockedUp = !Scp106LockedUp;
                    break;
                case RoleType.Scp049:
                    Scp049LockedUp = !Scp049LockedUp;
                    break;
                case RoleType.Scp173:
                    Scp173LockedUp = !Scp173LockedUp;
                    break;
                case RoleType.Scp93953:
                case RoleType.Scp93989:
                    Scp939LockedUp = !Scp939LockedUp;
                    break;
            }
        }

        public void ResetAllStates()
        {
            Scp079LockedUp = false;
            Scp096LockedUp = false;
            Scp106LockedUp = false;
            Scp049LockedUp = false;
            Scp939LockedUp = false;
            Scp173LockedUp = false;
        }
    }
}
