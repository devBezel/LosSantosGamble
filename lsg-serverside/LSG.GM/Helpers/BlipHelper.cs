using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.GM.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Helpers
{
    public static class BlipHelper
    {
        public static async Task CreateBlip(this IPlayer player, BlipModel blipModel) => await AltAsync.Do(() =>
        {
            player.EmitAsync("blip:create", blipModel.PosX, blipModel.PosY, blipModel.PosZ, (int)blipModel.Blip, (int)blipModel.Color, blipModel.Size, blipModel.Name, blipModel.ShortRange, blipModel.UniqueID);
        });

        public static async Task DeleteBlip(this IPlayer player, string uniqueID) => await AltAsync.Do(() =>
        {
            player.EmitAsync("blip:delete", uniqueID);
        });

        public static async Task CreateGlobalBlip(BlipModel blipModel) => await AltAsync.Do(() =>
        {
            AltAsync.EmitAllClients("blip:create", blipModel.PosX, blipModel.PosY, blipModel.PosZ, (int)blipModel.Blip, (int)blipModel.Color, blipModel.Size, blipModel.Name, blipModel.ShortRange, blipModel.UniqueID);
        });

        public static async Task DeleteGlobalBlip(string uniqueID) => await AltAsync.Do(() =>
        {
            AltAsync.EmitAllClients("blip:delete", uniqueID);
        });
    }
}
