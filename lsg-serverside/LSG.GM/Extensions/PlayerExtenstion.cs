using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class PlayerExtenstion
    {
        public static IPlayer GetPlayerById(int id)
        {
           IPlayer plr =  Alt.GetAllPlayers().SingleOrDefault(p => p.GetData("account:id", out int plrId) && plrId == id);

           return plr;
        }

        public static void SendSuccessNotify(this IPlayer player, string title = "Wykonano pomyślnie!", string message = "")
        {
            player.Emit("notify-server:success", title, message);
        }

        public static void SendErrorNotify(this IPlayer player, string title = "Wystąpił bląd!", string message = "")
        {
            player.Emit("notify-server:error", title, message);
        }
    }
}
