using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using Scp079Ev = Exiled.Events.Handlers.Scp079;
using Scp106Ev = Exiled.Events.Handlers.Scp106;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;

namespace ScpLockdown
{
    public class ScpLockdown : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        public override string Author { get; } = "Raul125";
        public override string Name { get; } = "ScpLockdown";
        public override string Prefix { get; } = "scp_lockdown";
        public override Version Version { get; } = new Version(2, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
        public static ScpLockdown Instance { get; private set; }
        public EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            Config.ParseCassies();
            Config.PreventDuplicatedCfgs();
            RegisterEvents();
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
            Scp106Ev.CreatingPortal += EventHandlers.OnCreatingPortal;
            Scp106Ev.Teleporting += EventHandlers.OnTeleporting;

            // Scp079 Events
            Scp079Ev.InteractingTesla += EventHandlers.OnInteractingTesla;
            Scp079Ev.ChangingCamera += EventHandlers.OnChangingCamera;
            Scp079Ev.ElevatorTeleporting += EventHandlers.OnElevatorTeleport;
            Scp079Ev.StartingSpeaker += EventHandlers.OnStartingSpeaker;
            Scp079Ev.TriggeringDoor += EventHandlers.OnInteractingDoor;
        }

        private void UnRegisterEvents()
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
            Scp106Ev.CreatingPortal -= EventHandlers.OnCreatingPortal;
            Scp106Ev.Teleporting -= EventHandlers.OnTeleporting;

            // Scp079 Events
            Scp079Ev.InteractingTesla -= EventHandlers.OnInteractingTesla;
            Scp079Ev.ChangingCamera -= EventHandlers.OnChangingCamera;
            Scp079Ev.ElevatorTeleporting -= EventHandlers.OnElevatorTeleport;
            Scp079Ev.StartingSpeaker -= EventHandlers.OnStartingSpeaker;
            Scp079Ev.TriggeringDoor -= EventHandlers.OnInteractingDoor;

            EventHandlers = null;
        }
    }
}
