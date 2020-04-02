using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core.Admin
{
    public class AdminBasePanel : IScript
    {
        //public AdminBasePanel()
        //{
        //    Alt.OnClient("admin-panel:teleportToAdmin", TeleportPlayerToAdmin);
        //    Alt.OnClient("admin-panel:teleportToPlayer", TeleportAdminToPlayer);
        //}

        [ClientEvent("admin-panel:teleportToPlayer")]
        private void TeleportAdminToPlayer(IPlayer sender, IPlayer getter)
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Administrator))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            //Jesli gracz wyjdzie podczas gdy admin kliknie na niego
            //IPlayer getter = (IPlayer)args[0];
            if (getter == null) return;

            sender.Position = getter.Position;
            sender.SendSuccessNotify(null, $"Przeteleportowałeś się do gracza");
        }
        [ClientEvent("admin-panel:teleportToAdmin")]
        private void TeleportPlayerToAdmin(IPlayer sender, IPlayer getter)
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Administrator))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }


            //IPlayer getter = (IPlayer)args[0];
            //Jesli gracz wyjdzie podczas gdy admin kliknie na niego
            if (getter == null) return;


            getter.Position = sender.Position;
            sender.SendSuccessNotify(null, $"Przeteleportowałeś gracza do siebie");
        }

        [Command("apanel")]
        public void OpenAdminBaseMenuCMD(IPlayer sender)
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            sender.Emit("admin:openBasePanel");
        }


    }
}
