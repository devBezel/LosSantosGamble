using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Helpers
{
    public static class BlipHelper
    {
        public static void CreateBlip(this IPlayer player, float posX, float posY, float posZ, int blip, int color, float size, string name, float shortRange, string uniqueID)
        {
            player.Emit("blip:create", posX, posY, posZ, (int)blip, (int)color, size, name, shortRange, uniqueID);
        }

        public static void DeleteBlip(this IPlayer player, string uniqueID)
        {
            player.Emit("blip:delete", uniqueID);
        }

        public static void CreateGlobalBlip(float posX, float posY, float posZ, int blip, int color, float size, string name, float shortRange, string uniqueID)
        {
            Alt.EmitAllClients("blip:create", posX, posY, posZ, (int)blip, (int)color, size, name, shortRange, uniqueID);
        }

        public static void DeleteGlobalBlip(string uniqueID)
        {
            Alt.EmitAllClients("blip:delete", uniqueID);
        }
    }
}
