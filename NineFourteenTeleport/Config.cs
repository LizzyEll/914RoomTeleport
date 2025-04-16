using Exiled.API.Enums;
using Exiled.API.Interfaces;
using Scp914;
using System.ComponentModel;
using PlayerRoles;

namespace NineFourteenTeleport;

    public class GiveEffect
    {
        public EffectType Effect { get; set; }
        public byte Intensity { get; set; }
        public byte Duration { get; set; }
    }
    
    public class Scp914Teleport
    {
        public uint Chance { get; set; } = 100;
        public GiveEffect[] Effects { get; set; } = [];
    }
    
    public sealed class Config : IConfig
    {
        [Description("Enables SCP-914 teleportation.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("List of the modes avaliable for teleportation [0=Rough ... 4=Very Fine] and the chance for teleportation out of 100 (eg. 1: 25).")]
        public Dictionary<Scp914KnobSetting, Scp914Teleport> Scp914Mode { get; set; } = 
            new ()
            {
                [Scp914KnobSetting.Coarse] = new Scp914Teleport() { 
                    Chance = 100,
                    Effects = [
                        new GiveEffect() { Effect = EffectType.Poisoned, Intensity = 2, Duration = 10 }
                    ]
                }
            };
        
        [Description("Determines what rooms can be teleported to using SCP-914 teleportation.")]
        public List<RoomType> TeleportRooms { get; set; } = [
            RoomType.LczCafe, 
            RoomType.LczCrossing, 
            RoomType.LczStraight, 
            RoomType.LczTCross, 
            RoomType.LczPlants, 
            RoomType.LczClassDSpawn, 
            RoomType.LczAirlock, 
            RoomType.LczCheckpointA,
            RoomType.LczCheckpointB,
            RoomType.HczTesla,
            RoomType.Hcz096,
            RoomType.Hcz106
        ];

        [Description("Determines what teams are allowed to use SCP-914 teleportation.")]
        public List<Team> AffectedTeams { get; set; } =
            [Team.ClassD, Team.Scientists, Team.FoundationForces, Team.ChaosInsurgency, Team.SCPs, Team.OtherAlive];
        
        [Description("If they go through on the specified TeleportMode but do not teleport, should the TeleportEffects listed above still be applied?")]
        public bool TeleportBackfire { get; set; } = true;
    }
