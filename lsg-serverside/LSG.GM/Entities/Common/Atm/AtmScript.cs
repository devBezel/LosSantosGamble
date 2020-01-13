using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Economy.Bank;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
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
        }

        private async Task OnColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;

            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;

            IPlayer player = targetEntity as IPlayer;

            if (player.IsInVehicle) return;

            AtmEntity atmEntity = colShape.GetAtmEntity();

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if(!characterEntity.DbModel.BankStatus)
            {
                player.SendErrorNotify("Los Santos Bank", "Nie masz konta w banku, aby je założyć udaj się do najbliżej placówki");
                return;
            }



            player.SendSuccessNotify(null, $"Witaj w ATM {characterEntity.DbModel.Name}");
            player.Emit("atm:information", characterEntity.DbModel.Name, characterEntity.DbModel.Surname, characterEntity.DbModel.Money, characterEntity.DbModel.Bank);
        });

        public void AtmDepositMoney(IPlayer player, object[] args)
        {
            int amount = (int)(long)args[0];

            BankHelper.DepositToBank(player, amount);
            player.SendSuccessNotify("Bank Los Santos", $"Przyjęto twoją wpłatę {amount}$ poprawnie");
        }

        public void AtmWithdrawMoney(IPlayer player, object[] args)
        {
            int amount = (int)(long)args[0];

            BankHelper.WithdrawFromBank(player, amount);
            player.SendSuccessNotify("Bank Los Santos", $"Wypłacono {amount}$ z bankomatu");
        }

    }
}
