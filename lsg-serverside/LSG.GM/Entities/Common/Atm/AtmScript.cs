using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Constant;
using LSG.GM.Economy.Bank;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtmModel = LSG.DAL.Database.Models.BankModels.Atm;

namespace LSG.GM.Entities.Common.Atm
{
    public class AtmScript :  IScript
    {

        public AtmScript()
        {
            Task.Run(() =>
            {
                AltAsync.OnColShape += OnColshape;
            });

            Alt.OnClient("atm:deposit", AtmDepositMoney);
            Alt.OnClient("atm:withdraw", AtmWithdrawMoney);
            Alt.OnClient("atm:openWindow", AtmOpenWindow);
        }

        private async Task OnColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;

            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;
            Alt.Log("Banki");

            AtmEntity atmEntity = colShape.GetAtmEntity();

            if (atmEntity == null || atmEntity.ColShape != colShape) return;

            IPlayer player = targetEntity as IPlayer;
            if (player.IsInVehicle) return;

            new Interaction(player, "atm:openWindow", "aby otworzyć ~g~ATM");
        });


        public void AtmOpenWindow(IPlayer player, object[] args)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if (!characterEntity.DbModel.BankStatus)
            {
                player.SendNativeNotify(null, NotificationNativeType.Bank, 1, "Nie masz konta w banku", "~g~ Bank Los Santos", $"Aby je założyć udaj się do najbliżej placówki");
                return;
            }


            player.Emit("atm:information", characterEntity.DbModel.Name, characterEntity.DbModel.Surname, characterEntity.DbModel.Money, characterEntity.DbModel.Bank);
        }

        [Command("createatm")]
        public async Task CreateAtmCMD(IPlayer sender) => await AltAsync.Do(async () =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            CharacterEntity characterEntity = sender.GetAccountEntity().characterEntity;
            AtmModel atm = new AtmModel()
            {
                PosX = sender.Position.X,
                PosY = sender.Position.Y,
                PosZ = sender.Position.Z - 0.9f,
                CreatorId = sender.GetAccountEntity().DbModel.Id
            };

            AtmEntity atmEntity = new AtmEntity(atm);
            await atmEntity.Spawn(true);

        });

        public void AtmDepositMoney(IPlayer player, object[] args)
        {
            int amount = (int)(long)args[0];

            BankHelper.DepositToBank(player, amount);
            player.SendNativeNotify(null, NotificationNativeType.Bank, 1, "Przyjęto wpłatę", "~g~ Bank Los Santos", $"Twoja wpłata {amount}$ została przyjęta poprawnie");
        }

        public void AtmWithdrawMoney(IPlayer player, object[] args)
        {
            int amount = (int)(long)args[0];

            BankHelper.WithdrawFromBank(player, amount);
            player.SendNativeNotify(null, NotificationNativeType.Bank, 1, "Przyjęto wypłatę", "~g~ Bank Los Santos", $"Twoja wypłata {amount}$ została przyjęta poprawnie");
        }

    }
}
