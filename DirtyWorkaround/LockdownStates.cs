using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScpLockdown.DirtyWorkaround
{
    public class LockdownStates
    {
        public static bool Scp079LockedUp { get; private set; }
        public static bool Scp096LockedUp { get; private set; }
        public static bool Scp106LockedUp { get; private set; }
        public static bool Scp049LockedUp { get; private set; }
        public static bool Scp939LockedUp { get; private set; }
        static LockdownStates()
        {
            Scp079LockedUp = false;
            Scp096LockedUp = false;
            Scp106LockedUp = false;
            Scp049LockedUp = false;
            Scp939LockedUp = false;
        }

        public void ToggleLockedUpState(RoleType role)
        {
            switch (role)
            {
                case RoleType.Scp079:
                    LockdownStates.Scp079LockedUp = !LockdownStates.Scp079LockedUp;
                    break;
                case RoleType.Scp096:
                    LockdownStates.Scp096LockedUp = !LockdownStates.Scp096LockedUp;
                    break;
                case RoleType.Scp106:
                    LockdownStates.Scp106LockedUp = !LockdownStates.Scp106LockedUp;
                    break;
                case RoleType.Scp049:
                    LockdownStates.Scp049LockedUp = !LockdownStates.Scp049LockedUp;
                    break;
                case RoleType.Scp93953:
                case RoleType.Scp93989:
                    LockdownStates.Scp939LockedUp = !LockdownStates.Scp939LockedUp;
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
        }
    }
}
