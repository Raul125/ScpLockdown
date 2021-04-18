using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using Scp079Ev = Exiled.Events.Handlers.Scp079;
using Scp106Ev = Exiled.Events.Handlers.Scp106;
using Exiled.API.Enums;
using Exiled.API.Features;
using ScpLockdown.EventHandlers;
using System;

namespace ScpLockdown
{
    public class ScpLockdown : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public static ScpLockdown Instance { get; private set; }

        public Handler Handler;

        public override string Author { get; } = "Raul125";

        public override string Name { get; } = "ScpLockdown";

        public override string Prefix { get; } = "ScpLockdown";

        public override Version Version { get; } = new Version(1, 0, 4);

        public override Version RequiredExiledVersion { get; } = new Version(2, 8, 0);

        public override void OnEnabled()
        {
            RegisterEvents();
            Instance = this;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Handler = new Handler(this);
            ServerEv.RoundStarted += Handler.OnRoundStart;
            ServerEv.WaitingForPlayers += Handler.OnWaitingForPlayers;
            ServerEv.RoundEnded += Handler.OnRoundEnded;
            ServerEv.RestartingRound += Handler.OnRoundRestarting;
            PlayerEv.ChangingRole += Handler.OnChangingRole;
            PlayerEv.EscapingPocketDimension += Handler.OnEscapingPocketDimension;
            PlayerEv.FailingEscapePocketDimension += Handler.OnFailingEscapePocketDimension;
            Scp106Ev.CreatingPortal += Handler.OnCreatingPortal;
            Scp106Ev.Teleporting += Handler.OnTeleporting;
            PlayerEv.InteractingDoor += Handler.OnInteractingDoor;
            Scp079Ev.InteractingTesla += Handler.OnInteractingTesla;
            Scp079Ev.ChangingCamera += Handler.OnChangingCamera;
            Scp079Ev.ElevatorTeleport += Handler.OnElevatorTeleport;
            Scp079Ev.StartingSpeaker += Handler.OnStartingSpeaker;
        }

        private void UnRegisterEvents()
        {
            ServerEv.RoundStarted -= Handler.OnRoundStart;
            ServerEv.WaitingForPlayers -= Handler.OnWaitingForPlayers;
            ServerEv.RoundEnded -= Handler.OnRoundEnded;
            ServerEv.RestartingRound -= Handler.OnRoundRestarting;
            PlayerEv.ChangingRole -= Handler.OnChangingRole;
            PlayerEv.EscapingPocketDimension -= Handler.OnEscapingPocketDimension;
            PlayerEv.FailingEscapePocketDimension -= Handler.OnFailingEscapePocketDimension;
            Scp106Ev.CreatingPortal -= Handler.OnCreatingPortal;
            Scp106Ev.Teleporting -= Handler.OnTeleporting;
            PlayerEv.InteractingDoor -= Handler.OnInteractingDoor;
            Scp079Ev.InteractingTesla -= Handler.OnInteractingTesla;
            Scp079Ev.ChangingCamera -= Handler.OnChangingCamera;
            Scp079Ev.ElevatorTeleport -= Handler.OnElevatorTeleport;
            Scp079Ev.StartingSpeaker -= Handler.OnStartingSpeaker;
            Handler.ResetAllStates();
            Handler = null;
        }
    }
}
