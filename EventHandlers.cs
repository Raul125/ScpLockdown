namespace ScpLockdown;

using System.Collections.Generic;
using MEC;
using Exiled.API.Features;
using Exiled.API.Enums;
using API.Extensions;
using API.Features;
using API.EventArgs;
using PlayerRoles;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Scp079;
using System.Linq;
using Exiled.API.Features.Doors;

public class EventHandlers(ScpLockdown ScpLockdown)
{
    private readonly ScpLockdown plugin = ScpLockdown;

    public void OnWaitingForPlayers()
    {
        LockdownController.ScpDoors.Add(RoleTypeId.Scp939, Room.Get(RoomType.Hcz939).Doors);
        LockdownController.ScpDoors.Add(RoleTypeId.Scp096, Room.Get(RoomType.Hcz096).Doors.Where(x => x.Type != DoorType.Scp096));
        LockdownController.ScpDoors.Add(RoleTypeId.Scp049, Door.List.Where(x => x.Type == DoorType.Scp049Gate));
        LockdownController.ScpDoors.Add(RoleTypeId.Scp173, Door.List.Where(x => x.Type == DoorType.Scp173NewGate));

        foreach (AffectedDoor affectedDoor in plugin.Config.AffectedDoors)
        {
            affectedDoor.Doors.Clear();
            affectedDoor.Doors.AddRange(Door.List.Where(x => x.Type == affectedDoor.DoorType));
        }

        Methods.LockAffectedDoors();
    }

    public IEnumerator<float> OnRoundStart()
    {
        Methods.ProcessDoors();
        Methods.SendCassies();

        yield return Timing.WaitForSeconds(1);

        foreach (var scp in plugin.Config.AffectedScps)
        {
            var state = scp.RoleType.LockedUpState();
            var ev = new TogglingLockedUpStateEventArgs(scp.RoleType, state, !state);
            if (!ev.IsAllowed)
                continue;

            LockdownController.ToggleLockedUpState(scp.RoleType);
            LockdownController.LockdownSCP(scp.RoleType, scp.TimeToUnlock);
        }
    }

    public void OnRoundEnded(RoundEndedEventArgs ev)
    {
        Timing.KillCoroutines(ScpLockdown.RunningCoroutines.ToArray());

        ScpLockdown.RunningCoroutines.Clear();
    }

    public void OnRoundRestarting()
    {
        Timing.KillCoroutines(ScpLockdown.RunningCoroutines.ToArray());

        LockdownController.ScpDoors.Clear();
        ScpLockdown.RunningCoroutines.Clear();

        LockdownController.ResetAllStates();
    }

    // Scp 106
    public void OnSpawning(SpawningEventArgs ev)
    {
        if (RoleTypeId.Scp106.LockedUpState() && ev.Player.Role.Type == RoleTypeId.Scp106)
            ev.Position = Extensions.PocketDimensionPosition;
    }

    public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
    {
        if (ev.Player.Role.Type is RoleTypeId.Scp106 && RoleTypeId.Scp106.LockedUpState())
        {
            ev.Player.SendToPocketDimension();
            ev.IsAllowed = false;
        }
    }

    public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
    {
        if (ev.Player.Role.Type is RoleTypeId.Scp106 && RoleTypeId.Scp106.LockedUpState())
        {
            ev.Player.SendToPocketDimension();
            ev.IsAllowed = false;
        }
    }

    public void OnTeleporting(TeleportingEventArgs ev)
    {
        if (RoleTypeId.Scp106.LockedUpState())
            ev.IsAllowed = false;
    }

    // Scp 079
    public void OnInteractingTesla(InteractingTeslaEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState())
            ev.IsAllowed = false;
    }

    public void OnInteractingDoor(TriggeringDoorEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && ev.Player.Role.Type is RoleTypeId.Scp079)
            ev.IsAllowed = false;
    }

    public void OnChangingCamera(ChangingCameraEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && !plugin.Config.Scp079Camera)
            ev.IsAllowed = false;
    }

    public void OnElevatorTeleport(ElevatorTeleportingEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState() && !plugin.Config.Scp079Camera)
            ev.IsAllowed = false;
    }

    public void OnChangingSpeakerStatus(ChangingSpeakerStatusEventArgs ev)
    {
        if (RoleTypeId.Scp079.LockedUpState())
            ev.IsAllowed = false;
    }
}
