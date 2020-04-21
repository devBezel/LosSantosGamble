using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Extensions
{
    public static class NativeExtenstion
    {

        #region Player
        // help: Freezuje gracza
        public static void FreezePosition(this IPlayer player, bool toggle)
        {
            player.Emit("native-extenstion:freezeEntityPosition", toggle);
        }


        #endregion

    }
}
