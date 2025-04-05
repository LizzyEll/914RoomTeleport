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
        if (NineFourteenTeleport.Instance == null) return;
        if (!NineFourteenTeleport.Instance.Config.IsEnabled) return;
        if (!NineFourteenTeleport.Instance.Config.Scp914Mode.Keys.Contains(ev.KnobSetting)) return;

        var roomIndex = _rnd.Next(0, NineFourteenTeleport.Instance.Config.TeleportRooms.Count());
        var roomType = NineFourteenTeleport.Instance.Config.TeleportRooms[roomIndex];
        var randomRoom = Room.Get(roomType);

        var config = NineFourteenTeleport.Instance.Config.Scp914Mode[ev.KnobSetting]!;

        var randomNum = _rnd.NextDouble();
        Log.Info($"Number: {randomNum} <= Chance: {config.Chance / 100d}");
        
        if (randomNum <= config.Chance / 100d) {
            Log.Info("Teleport Success!");
            
            Timing.CallDelayed(0.1f, () => {
                ev.Player.Position = randomRoom.Position + Vector3.up * 2;
                ApplyEffects(ev.Player, config.Effects);
            });
        }
        else
        {
            if (!NineFourteenTeleport.Instance.Config.TeleportBackfire) return;
            ApplyEffects(ev.Player, config.Effects);
        }
        Log.Info($"Teleporting on {ev.KnobSetting}: {ev.Player.Nickname}");
    }
}