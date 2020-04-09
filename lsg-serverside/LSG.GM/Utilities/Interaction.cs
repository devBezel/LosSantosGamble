using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Utilities
{
    public class Interaction
    {
        public Interaction(IPlayer player, string eventName, string text, object interactionArgs = null)
        {
            player.Emit("interaction", eventName, text, interactionArgs);
        }

        public static void Clear(IPlayer player)
        {
            player.Emit("interaction:clear");
        }
    }
}
