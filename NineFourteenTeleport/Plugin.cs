using Exiled.API.Enums;
using Exiled.API.Features;
using Events = Exiled.Events.Handlers;

namespace NineFourteenTeleport;

    public class NineFourteenTeleport : Plugin<Config>
    {
        public override string Name => "Scp914Teleportation";
        public override string Author => "Thunder, Recreation by Lizzy";
        public override Version Version => new Version(1, 0, 0);

        public static NineFourteenTeleport? Instance { get; private set; }

        public override PluginPriority Priority => PluginPriority.Low;

        private readonly EventHandlers _handlers = new ();

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;
            Instance = this;

            Events.Scp914.UpgradingPlayer += _handlers.OnUpgradePlayer;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            if (!Config.IsEnabled) return;
            Instance = null;

            Events.Scp914.UpgradingPlayer -= _handlers.OnUpgradePlayer;
            base.OnDisabled();
        }
    }
