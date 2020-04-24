using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Wrapper
{
    public static class LSGNativeWrapper
    {
        #region Player
        // help: Freezuje gracza
        public static void FreezePosition(this IPlayer player, bool toggle)
        {
            player.Emit("native-wrapper:freezeEntityPosition", toggle);
        }

        public static void PlayAnimation(this IPlayer player, string animDic, string animation, int time, int animFlag = 1)
        {
            player.Emit("native-wrapper:playAnimation", animDic, animation, time, animFlag);
        }

        public static void SetEnableHandcuffs(this IPlayer player, bool toggle)
        {
            player.Emit("native-wrapper:setEnableHandcuffs", toggle);
        }

        public static void ClearTasks(this IPlayer player)
        {
            player.Emit("native-wrapper:clearPedTasks");
        }

        public static void DisablePlayerFiring(this IPlayer player, bool toggle)
        {
            player.Emit("native-wrapper:disablePlayerFiring", toggle);
        }

        public static void SetCanPlayGestureAnims(this IPlayer player, bool toggle)
        {
            player.Emit("native-wrapper:setPedCanPlayGestureAnims", toggle);
        }

        #endregion
    }
}
