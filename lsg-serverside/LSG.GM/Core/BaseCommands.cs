using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core
{
    public class BaseCommands : IScript
    {

        [Command("podnies")]
        public void ReviveCMD(IPlayer player)
        {
            player.Emit("player:help");
        }
    }
}
