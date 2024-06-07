using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using Scp106Ev = Exiled.Events.Handlers.Scp106;
using Scp079Ev = Exiled.Events.Handlers.Scp079;

namespace ScpLockdown;

public class ScpLockdown : Plugin<Config>
{
    public static readonly List<CoroutineHandle> RunningCoroutines = [];

    private EventHandlers _eventHandlers;
    public override PluginPriority Priority => PluginPriority.Medium;
    public override string Author => "Raul125";
    public override string Name => "ScpLockdown";
    public override string Prefix => "scp_lockdown";
    public override Version Version { get; } = new(3, 2, 1);
    public override Version RequiredExiledVersion { get; } = new(8, 9, 4);

    public static ScpLockdown Instance { get; private set; }

    public override void OnEnabled()
    {
        Instance = this;
        _eventHandlers = new EventHandlers(this);

        ServerEv.RoundStarted += _eventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers += _eventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded += EventHandlers.OnRoundEnded;
        ServerEv.RestartingRound += EventHandlers.OnRoundRestarting;

        PlayerEv.Spawning += EventHandlers.OnSpawning;
        PlayerEv.EscapingPocketDimension += EventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension += EventHandlers.OnFailingEscapePocketDimension;

        Scp106Ev.Teleporting += EventHandlers.OnTeleporting;

        Scp079Ev.InteractingTesla += EventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera += _eventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting += _eventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus += EventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor += EventHandlers.OnInteractingDoor;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        ServerEv.RoundStarted -= _eventHandlers.OnRoundStart;
        ServerEv.WaitingForPlayers -= _eventHandlers.OnWaitingForPlayers;
        ServerEv.RoundEnded -= EventHandlers.OnRoundEnded;
        ServerEv.RestartingRound -= EventHandlers.OnRoundRestarting;

        PlayerEv.Spawning -= EventHandlers.OnSpawning;
        PlayerEv.EscapingPocketDimension -= EventHandlers.OnEscapingPocketDimension;
        PlayerEv.FailingEscapePocketDimension -= EventHandlers.OnFailingEscapePocketDimension;

        Scp106Ev.Teleporting -= EventHandlers.OnTeleporting;

        Scp079Ev.InteractingTesla -= EventHandlers.OnInteractingTesla;
        Scp079Ev.ChangingCamera -= _eventHandlers.OnChangingCamera;
        Scp079Ev.ElevatorTeleporting -= _eventHandlers.OnElevatorTeleport;
        Scp079Ev.ChangingSpeakerStatus -= EventHandlers.OnChangingSpeakerStatus;
        Scp079Ev.TriggeringDoor -= EventHandlers.OnInteractingDoor;

        _eventHandlers = null;
        Instance = null;

        base.OnDisabled();
    }
}