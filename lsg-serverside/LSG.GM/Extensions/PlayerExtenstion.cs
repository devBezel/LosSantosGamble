using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class PlayerExtenstion
    {
        public static void SendSuccessNotify(this IPlayer player, string title, string message)
        {
            player.Emit("notify-server:success", title, message);
        }

        public static void SendErrorNotify(this IPlayer player, string title, string message)
        {
            player.Emit("notify-server:error", title, message);
        }
    }
}
