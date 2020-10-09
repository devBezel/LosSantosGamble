using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models.GroupModels;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Enums;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static CharacterEntity GetPlayerByCharacterId(int? id)
        {
            IPlayer plr = Alt.GetAllPlayers().SingleOrDefault(c => c.GetData("account:data", out AccountEntity account) && account.characterEntity.DbModel.Id == id);
            return plr.GetAccountEntity().characterEntity;
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

        public static void SendChatMessageInfo(this IPlayer player, string message)
        {
            player.SendChatMessage("[{0c6b00}INFO{ffffff}] " + message);
        }

        public static void SendChatMessageError(this IPlayer player, string message)
        {
            player.SendChatMessage("[{ba0000}BLĄD{ffffff}] " + message);
        }

        public static void SendChatMessageToNearbyPlayers(this IPlayer player, string message, ChatType type = ChatType.Normal)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            IEnumerable<IPlayer> players = Alt.GetAllPlayers().Where(x => Calculation.Distance(player.Position, x.Position) <= 5);

            foreach (IPlayer plr in players)
            {
                switch (type)
                {
                    case ChatType.Normal: 
                        plr.SendChatMessage(message);
                        return;
                    case ChatType.Me:
                        plr.SendChatMessage("{de59d1}** " + characterEntity.DbModel.Name + " " + characterEntity.DbModel.Surname + " " + message + ".");
                        return;
                    case ChatType.Do:
                        plr.SendChatMessage("{877485}** " + message + ". " + "(( " + characterEntity.DbModel.Name + " " + characterEntity.DbModel.Surname + " ))**");
                        return;
                    default:
                        break;
                }
            }
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
                Alt.Log($"groups[slot]: {groups[slot].DbModel.Name}");

                group = slot < groups.Count ? groups[slot] : null;
                Alt.Log($"accountEntity.characterEntity.DbModel.GroupWorkers: {accountEntity.characterEntity.DbModel.GroupWorkers.Count()}");
                groupWorker = accountEntity.characterEntity.DbModel.GroupWorkers.SingleOrDefault(g => g.GroupId == groups[slot].DbModel.Id);

            }
            Alt.Log($"group: {group.DbModel.Name}");
            return group != null && groupWorker != null;
        }
    }
}
