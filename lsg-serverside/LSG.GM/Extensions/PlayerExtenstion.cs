using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class PlayerExtenstion
    {
        public static AccountEntity GetAccountEntity(this IPlayer player)
        {
            player.GetData("account:data", out AccountEntity account);

            return account;
        }

        public static IPlayer GetPlayerById(int id)
        {
           IPlayer plr =  Alt.GetAllPlayers().SingleOrDefault(p => p.GetData("account:id", out int plrId) && plrId == id);
           
           return plr;
        }

        public static void SendSuccessNotify(this IPlayer player, string title = "Wykonano pomyślnie!", string message = "")
        {
            player.Emit("notify:success", title, message);
        }

        public static void SendErrorNotify(this IPlayer player, string title = "Wystąpił bląd!", string message = "")
        {
            player.Emit("notify:error", title, message);
        }

        public static void SendWarningNotify(this IPlayer player, string title = "Wystąpił bląd!", string message = "")
        {
            player.Emit("notify:warning", title, message);
        }

        public static void SendNativeNotify(this IPlayer player, int? backgroundColor, string notifyImage, int iconType, string title, string subtitle,string  message, int durationMult = 1)
        {
            player.Emit("notify:native", backgroundColor, notifyImage, iconType, title, subtitle, message, durationMult);
        }
    }
}
