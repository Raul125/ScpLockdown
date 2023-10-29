namespace SCPLockdown;

using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using Scp079Ev = Exiled.Events.Handlers.Scp079;
using Scp106Ev = Exiled.Events.Handlers.Scp106;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;

public class SCPLockdown : Plugin<Config>
{
    public override PluginPriority Priority { get; } = PluginPriority.Medium;
    public override string Author { get; } = "Raul125";
    public override string Name { get; } = "SCPLockdown";
    public override string Prefix { get; } = "scp_lockdown";
    public override Version Version { get; } = new Version(3, 1, 1);
    public override Version RequiredExiledVersion { get; } = new Version(8, 3, 1);
    public static SCPLockdown Instance { get; private set; }
    public EventHandlers EventHandlers { get; private set; }

    public override void OnEnabled()
    {
        Instance = this;
        EventHandlers = new EventHandlers(this);

        // Server Events
        ServerEv.RoundStarted += EventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded += EventHandlers.OnRoundEnded;
        ServerEv.RestartingRound += EventHandlers.OnRoundRestarting;

        // Player Events
        PlayerEv.ChangingRole += EventHandlers.OnChangingRole;
        PlayerEv.EscapingPocketDimension += EventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension += EventHandlers.OnFailingEscapePocketDimension;

        // Scp106 Events
        Scp106Ev.Teleporting += EventHandlers.OnTeleporting;

        // Scp079 Events
        Scp079Ev.InteractingTesla += EventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera += EventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting += EventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus += EventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor += EventHandlers.OnInteractingDoor;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        // Server Events
        ServerEv.RoundStarted -= EventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded -= EventHandlers.OnRoundEnded;
        ServerEv.RestartingRound -= EventHandlers.OnRoundRestarting;

        // Player Events
        PlayerEv.ChangingRole -= EventHandlers.OnChangingRole;
        PlayerEv.EscapingPocketDimension -= EventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension -= EventHandlers.OnFailingEscapePocketDimension;

        // Scp106 Events
        Scp106Ev.Teleporting -= EventHandlers.OnTeleporting;

        // Scp079 Events
        Scp079Ev.InteractingTesla -= EventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera -= EventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting -= EventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus -= EventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor -= EventHandlers.OnInteractingDoor;

        EventHandlers = null;
        Instance = null;

        base.OnDisabled();
    }
}
