using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities;
using LSG.GM.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Helpers
{
    public static class DrawTextHelper
    {
        public static void CreateDrawText(this IPlayer player, DrawTextModel drawTextModel)
        {
            player.EmitAsync("drawText:create", drawTextModel);
        }

        public static void RemoveDrawText(this IPlayer player, string uniqueID)
        {
            player.Emit("drawText:remove", uniqueID);
        }

        public static void CreateGlobalDrawText(DrawTextModel drawTextModel)
        {
            EntityHelper.Add(drawTextModel);
            Alt.EmitAllClients("drawText:create", drawTextModel);
        }

        public static void RemoveGlobalDrawText(string uniqueID)
        {
            EntityHelper.RemoveVehicleTrunkDrawText(uniqueID);
            Alt.EmitAllClients("drawText:remove", uniqueID);
        }

    }
}
