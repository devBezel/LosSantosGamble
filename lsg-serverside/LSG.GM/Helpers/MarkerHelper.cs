using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Helpers
{
    public static class MarkerHelper
    {
        public static async Task CreateMarker(this IPlayer player, MarkerModel marker) => await AltAsync.Do(() =>
        {
            player.EmitAsync("marker:create", marker);
        });

        public static async Task CreateGlobalMarker(MarkerModel marker) => await AltAsync.Do(() =>
        {
            AltAsync.EmitAllClients("marker:create", marker);
        });

        public static async Task RemoveGlobalMarker(string uniqueID) => await AltAsync.Do(() =>
        {
            AltAsync.EmitAllClients("marker:remove", uniqueID);
        });
    }
}
