namespace ScpLockdown;

using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using Scp079Ev = Exiled.Events.Handlers.Scp079;
using Scp106Ev = Exiled.Events.Handlers.Scp106;
using MEC;

public class ScpLockdown : Plugin<Config>
{
    public override PluginPriority Priority { get; } = PluginPriority.Medium;
    public override string Author { get; } = "Raul125";
    public override string Name { get; } = "ScpLockdown";
    public override string Prefix { get; } = "scp_lockdown";
    public override Version Version { get; } = new Version(3, 2, 0);
    public override Version RequiredExiledVersion { get; } = new Version(8, 8, 0);

    public static ScpLockdown Instance { get; private set; }
    public static readonly List<CoroutineHandle> RunningCoroutines = [];

    private EventHandlers eventHandlers;

    public override void OnEnabled()
    {
        Instance = this;
        eventHandlers = new EventHandlers(this);

        ServerEv.RoundStarted += eventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded += eventHandlers.OnRoundEnded;
        ServerEv.RestartingRound += eventHandlers.OnRoundRestarting;

        PlayerEv.Spawning += eventHandlers.OnSpawning;
        PlayerEv.EscapingPocketDimension += eventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension += eventHandlers.OnFailingEscapePocketDimension;

        Scp106Ev.Teleporting += eventHandlers.OnTeleporting;

        Scp079Ev.InteractingTesla += eventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera += eventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting += eventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus += eventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor += eventHandlers.OnInteractingDoor;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        ServerEv.RoundStarted -= eventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded -= eventHandlers.OnRoundEnded;
        ServerEv.RestartingRound -= eventHandlers.OnRoundRestarting;

        PlayerEv.Spawning -= eventHandlers.OnSpawning;
        PlayerEv.EscapingPocketDimension -= eventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension -= eventHandlers.OnFailingEscapePocketDimension;

        Scp106Ev.Teleporting -= eventHandlers.OnTeleporting;

        Scp079Ev.InteractingTesla -= eventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera -= eventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting -= eventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus -= eventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor -= eventHandlers.OnInteractingDoor;

        eventHandlers = null;
        Instance = null;

        base.OnDisabled();
    }
}
