using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Admin
{
    public static class AdminEntity
    {
        public static void ChangeAdminDutyState(this IPlayer player)
        {
            if(player.OnAdminDuty())
            {
                player.SetData("admin:duty", false);
                player.Emit("admin:setDuty", false);
                return;
            }
            player.SetData("admin:duty", true);
            player.Emit("admin:setDuty", true);
        }

        public static bool OnAdminDuty(this IPlayer player)
        {
            player.GetData("admin:duty", out bool onDuty);

            if (!onDuty)
            {
                return false;
            }

            return true;
        }
    }
}
