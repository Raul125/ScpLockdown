using EXPlayerEvents = Exiled.Events.Handlers.Player;
using EXServerEvents = Exiled.Events.Handlers.Server;
using EX079Events = Exiled.Events.Handlers.Scp079;
using EX106Events = Exiled.Events.Handlers.Scp106;
using Exiled.API.Enums;
using Exiled.API.Features;
using ScpLockdown.EventHandlers;
using System;
using ScpLockdown.Helper;

namespace ScpLockdown
{
    public class ScpLockdown : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private ScpLockdown()
        {
        }

        public static ScpLockdown Instance { get; } = new ScpLockdown();

        public RoundHandler _lockdownHandler;

        public Methods methods;

        public override string Author { get; } = "Raul125";

        public override string Name { get; } = "ScpLockdown";

        public override string Prefix { get; } = "ScpLockdown";

        public override Version Version { get; } = new Version(1, 0, 3);

        public override Version RequiredExiledVersion { get; } = new Version(2, 8, 0);

        public override void OnEnabled()
        {
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _lockdownHandler = new RoundHandler(this);
            methods = new Methods(this);

            EXServerEvents.RoundStarted += _lockdownHandler.OnRoundStart;
            EXServerEvents.WaitingForPlayers += _lockdownHandler.OnWaitingForPlayers;
            EXPlayerEvents.ChangingRole += _lockdownHandler.OnChangingRole;
            EXPlayerEvents.EscapingPocketDimension += _lockdownHandler.OnEscapingPocketDimension;
            EXPlayerEvents.FailingEscapePocketDimension += _lockdownHandler.OnFailingEscapePocketDimension;
            EX106Events.CreatingPortal += _lockdownHandler.OnCreatingPortal;
            EX106Events.Teleporting += _lockdownHandler.OnTeleporting;
            EX079Events.InteractingDoor += _lockdownHandler.OnInteractingDoor;
            EX079Events.InteractingTesla += _lockdownHandler.OnInteractingTesla;
            EX079Events.ChangingCamera += _lockdownHandler.OnChangingCamera;
            EX079Events.ElevatorTeleport += _lockdownHandler.OnElevatorTeleport;
            EX079Events.StartingSpeaker += _lockdownHandler.OnStartingSpeaker;
            EXServerEvents.RoundEnded += _lockdownHandler.OnRoundEnded;
        }
        private void UnRegisterEvents()
        {
            EXServerEvents.RoundStarted -= _lockdownHandler.OnRoundStart;
            EXServerEvents.WaitingForPlayers -= _lockdownHandler.OnWaitingForPlayers;
            EXPlayerEvents.ChangingRole -= _lockdownHandler.OnChangingRole;
            EXPlayerEvents.EscapingPocketDimension -= _lockdownHandler.OnEscapingPocketDimension;
            EXPlayerEvents.FailingEscapePocketDimension -= _lockdownHandler.OnFailingEscapePocketDimension;
            EX106Events.CreatingPortal -= _lockdownHandler.OnCreatingPortal;
            EX106Events.Teleporting -= _lockdownHandler.OnTeleporting;
            EX079Events.InteractingDoor -= _lockdownHandler.OnInteractingDoor;
            EX079Events.InteractingTesla -= _lockdownHandler.OnInteractingTesla;
            EX079Events.ChangingCamera -= _lockdownHandler.OnChangingCamera;
            EX079Events.ElevatorTeleport -= _lockdownHandler.OnElevatorTeleport;
            EX079Events.StartingSpeaker -= _lockdownHandler.OnStartingSpeaker;
            EXServerEvents.RoundEnded -= _lockdownHandler.OnRoundEnded;

            _lockdownHandler.ResetAllStates();
            _lockdownHandler = null;
            methods = null;
        }
    }
}
