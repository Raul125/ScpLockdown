using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using ScpLockdown.API;
using ScpLockdown.API.EventArgs;
using ScpLockdown.API.Features;
using UnityEngine;

namespace ScpLockdown;

public class EventHandlers(ScpLockdown scpLockdown)
{
    public void OnWaitingForPlayers()
    {
        LockdownController.ScpDoors.Add(RoleTypeId.Scp939, Room.Get(RoomType.Hcz939).Doors);
        LockdownController.ScpDoors.Add(RoleTypeId.Scp096,
                                        Room.Get(RoomType.Hcz096).Doors.Where(x => x.Type != DoorType.Scp096));
        LockdownController.ScpDoors.Add(RoleTypeId.Scp049, Door.List.Where(x => x.Type == DoorType.Scp049Gate));
        LockdownController.ScpDoors.Add(RoleTypeId.Scp173, Door.List.Where(x => x.Type == DoorType.Scp173NewGate));

        foreach (var affectedDoor in scpLockdown.Config.AffectedDoors)
        {
            affectedDoor.Doors.Clear();
            affectedDoor.Doors.AddRange(Door.List.Where(x => x.Type == affectedDoor.DoorType));
        }

        Methods.LockAffectedDoors();
    }

    public void OnRoundStart()
    {
        Methods.ProcessDoors();
        Methods.SendCassies();

        foreach (var scp in from scp in scpLockdown.Config.AffectedScps
                            let state = scp.RoleType.LockedUpState()
                            let ev = new TogglingLockedUpStateEventArgs(scp.RoleType, state, !state)
                            where ev.IsAllowed
                            select scp)
        {
            LockdownController.LockdownScp(scp.RoleType, scp.TimeToUnlock);
        }
    }

    public static void OnRoundEnded(RoundEndedEventArgs ev)
    {
        Timing.KillCoroutines(ScpLockdown.RunningCoroutines.ToArray());

        ScpLockdown.RunningCoroutines.Clear();
    }

    public static void OnRoundRestarting()
    {
        Timing.KillCoroutines(ScpLockdown.RunningCoroutines.ToArray());

        LockdownController.ScpDoors.Clear();
        ScpLockdown.RunningCoroutines.Clear();

        LockdownController.ResetAllStates();
    }

    // Scp 106
    public static void OnSpawning(SpawningEventArgs ev)
    {
        if (RoleTypeId.Scp106.LockedUpState() && ev.Player.Role.Type == RoleTypeId.Scp106)
            ev.Position = Room.Get(RoomType.Pocket).Position + Vector3.up;
    }

    public static void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
    {
        if (ev.Player.Role.Type is not RoleTypeId.Scp106 || !RoleTypeId.Scp106.LockedUpState())
            return;

        ev.Player.Teleport(RoomType.Pocket);
        ev.IsAllowed = false;
    }

    public static void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
    {
        if (ev.Player.Role.Type is not RoleTypeId.Scp106 || !RoleTypeId.Scp106.LockedUpState())
            return;

        ev.Player.Teleport(RoomType.Pocket);
        ev.IsAllowed = false;
    }

    public static void OnTeleporting(TeleportingEventArgs ev)
    {
        if (RoleTypeId.Scp106.LockedUpState())
            ev.IsAllowed = false;
    }

    // Scp 079
    public static void OnInteractingTesla(InteractingTeslaEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState())
            ev.IsAllowed = false;
    }

    public static void OnInteractingDoor(TriggeringDoorEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && ev.Player.Role.Type is RoleTypeId.Scp079)
            ev.IsAllowed = false;
    }

    public void OnChangingCamera(ChangingCameraEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && !scpLockdown.Config.Scp079Camera)
            ev.IsAllowed = false;
    }

    public void OnElevatorTeleport(ElevatorTeleportingEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && !scpLockdown.Config.Scp079Camera)
            ev.IsAllowed = false;
    }

    public static void OnChangingSpeakerStatus(ChangingSpeakerStatusEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState())
            ev.IsAllowed = false;
    }
}