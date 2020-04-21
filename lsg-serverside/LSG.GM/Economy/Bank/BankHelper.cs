using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Bank
{
    public static class BankHelper
    {
        public static void DepositToBank(IPlayer player, int amount)
        {
            CharacterEntity character = player.GetAccountEntity().characterEntity;

            if (!character.HasEnoughMoney(amount, false)) return;

            character.DbModel.Money -= amount;
            character.DbModel.Bank += amount;
        }

        public static void WithdrawFromBank(IPlayer player, int amount)
        {
            CharacterEntity character = player.GetAccountEntity().characterEntity;

            if (character.DbModel.Bank < amount) return;

            character.DbModel.Bank -= amount;
            character.DbModel.Money += amount;
        }
    }
}
