using HarmonyLib;
using ScpLockdown.DirtyWorkaround;
using System;
using UnityEngine;

namespace ScpLockdown.Patch
{
    [HarmonyPatch(typeof(Scp079PlayerScript), nameof(Scp079PlayerScript.CallCmdInteract))]
    internal static class Scp079Patch
    {
        private static bool Prefix(string command, GameObject target)
        {
            return (String.Equals(command.Split(':')[0], "DOORLOCK") || String.Equals(command.Split(':')[0], "ELEVATORUSE")) && LockdownStates.Scp079LockedUp ? false : true;
        }
    }
}
