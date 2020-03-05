using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public class NativeExtenstion : IScript
    {
        public NativeExtenstion()
        {
            //Alt.OnClient("item:weapon-ammo", GetCurrentWeaponAmmo);
        }

        private void GetCurrentWeaponAmmo(IPlayer player, object[] args)
        {
            Alt.Log("Wykonuje się getcurrentweaponammo");
            Alt.Log("Current ammo: " + (int)(long)args[0]);
            player.SetData("item:weapon-ammo", (int)(long)args[0]);
            
        }
    }
}
