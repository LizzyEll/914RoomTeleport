using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp914;
using MEC;
using UnityEngine;
using Random = System.Random;

namespace NineFourteenTeleport;

public class EventHandlers {
    private readonly Random _rnd = new();

    private static void ApplyEffects(Player plr, GiveEffect[] effects)
    {
        Timing.CallDelayed(Timing.WaitForOneFrame, () => {
            effects.ForEach((effect) => {
                plr.EnableEffect(effect.Effect, effect.Intensity, effect.Duration);
            });
        });
    }
    public void OnUpgradePlayer(UpgradingPlayerEventArgs ev)
    {
        /* Make sure the plugin is still enabled */
        if (NineFourteenTeleport.Instance == null) return;
        if (!NineFourteenTeleport.Instance.Config.IsEnabled) return;
        
        /* Check if the 914 Knob Setting is enabled in the config */
        if (!NineFourteenTeleport.Instance.Config.Scp914Mode.Keys.Contains(ev.KnobSetting)) return;

        /* Get a random room for the player to teleport into */
        var roomIndex = _rnd.Next(0, NineFourteenTeleport.Instance.Config.TeleportRooms.Count());
        var roomType = NineFourteenTeleport.Instance.Config.TeleportRooms[roomIndex];
        var randomRoom = Room.Get(roomType);

        var config = NineFourteenTeleport.Instance.Config.Scp914Mode[ev.KnobSetting]!;
        var randomNum = _rnd.NextDouble();

        /* Did the teleport succeed? */
        var success = randomNum <= config.Chance / 100d;
        
        if (!success && !NineFourteenTeleport.Instance.Config.TeleportBackfire) return;
        if (success)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                /* Teleport the player */
                ev.Player.Position = randomRoom.Position + Vector3.up * 2;
            });
        }

        ApplyEffects(ev.Player, config.Effects);
    }
}