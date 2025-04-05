/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;

namespace NineFourteenTeleport
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class RACommands : ICommand
    {
        public string Command { get; set; } = "914tp";

        public string[] Aliases { get; set; } = { };

        public string Description { get; set; } = "Main command for SCP-914 teleportation.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count() == 0)
            {
                response = "Missing required first argument (must be 'enable' or 'disable').";
                return false;
            }

            switch (arguments.At(0))
            {
                case "enable":
                    if (!((CommandSender)sender).CheckPermission("scp914tp.settings"))
                    {
                        response = "Access denied.";
                        return false;
                    }
                    NineFourteenTeleport.enabledInGame = true;
                    response = "Successfully enabled SCP-914 Teleportation.";
                    break;
                case "disable":
                    if (!((CommandSender)sender).CheckPermission("scp914tp.settings"))
                    {
                        response = "Access denied.";
                        return false;
                    }
                    NineFourteenTeleport.enabledInGame = false;
                    response = "Successfully disabled SCP-914 Teleportation.";
                    break;
                default:
                    response = $"Unknown parameter '{arguments.At(0)}'";
                    break;
            }

            return true;
        }
    }
}*/