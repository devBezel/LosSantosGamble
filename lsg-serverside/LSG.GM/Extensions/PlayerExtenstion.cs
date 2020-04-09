using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.GroupModels;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class PlayerExtenstion
    {
        public static AccountEntity GetAccountEntity(this IPlayer player)
        {
            player.GetData("account:data", out AccountEntity account);

            return account;
        }

        public static IPlayer GetPlayerById(int id)
        {
           IPlayer plr =  Alt.GetAllPlayers().SingleOrDefault(p => p.GetData("account:id", out int plrId) && plrId == id);
           
           return plr;
        }

        public static AccountEntity GetPlayerByAccountId(int id)
        {
            IPlayer plr = Alt.GetAllPlayers().SingleOrDefault(c => c.GetData("account:data", out AccountEntity account) && account.DbModel.Id == id);
            return plr.GetAccountEntity();
        }

        public static void SendSuccessNotify(this IPlayer player, string title = "Wykonano pomyślnie!", string message = "")
        {
            player.Emit("notify:success", title, message);
        }

        public static void SendErrorNotify(this IPlayer player, string title = "Wystąpił bląd!", string message = "")
        {
            player.Emit("notify:error", title, message);
        }

        public static void SendWarningNotify(this IPlayer player, string title = "Wystąpił bląd!", string message = "")
        {
            player.Emit("notify:warning", title, message);
        }

        public static void SendNativeNotify(this IPlayer player, int? backgroundColor, string notifyImage, int iconType, string title, string subtitle,string  message, int durationMult = 1)
        {
            player.Emit("notify:native", backgroundColor, notifyImage, iconType, title, subtitle, message, durationMult);
        }

        public static bool TryGetGroupByUnsafeSlot(this IPlayer player, short slot, out GroupEntity group, out GroupWorkerModel groupWorker)
        {
            group = null;
            groupWorker = null;

            if(slot > 0 || slot <= 3)
            {
                AccountEntity accountEntity = player.GetAccountEntity();

                slot--;
                List<GroupEntity> groups = EntityHelper.GetPlayerGroups(accountEntity).ToList();
                Alt.Log($"GROUPS: {groups.Count}");
                group = slot < groups.Count ? groups[slot] : null;
                groupWorker = accountEntity.characterEntity.DbModel.GroupWorkers.SingleOrDefault(g => g.Id == groups[slot].Id);

            }

            return group != null && groupWorker != null;
        }
    }
}
