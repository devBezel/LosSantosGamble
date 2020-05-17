using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Wrapper
{
    public static class LSGNativeWrapper
    {
        #region Player
        
        public static void PlayAnimation(this IPlayer player, string animDic, string animation, int time, int animFlag = 1)
        {
            player.Emit("native-wrapper:playAnimation", animDic, animation, time, animFlag);
        }
        //TODO: Dorobić callback
        public static void CallNative(this IPlayer player, string native, object[] args = null)
        {
            player.Emit("native-wrapper:callNative", native, args);
        }
        #endregion
    }
}
