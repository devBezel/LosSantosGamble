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

            character.RemoveMoney(amount, false);
            character.AddMoney(amount, true);
        }

        public static void WithdrawFromBank(IPlayer player, int amount)
        {
            CharacterEntity character = player.GetAccountEntity().characterEntity;

            if (character.DbModel.Bank < amount) return;

            character.RemoveMoney(amount, true);
            character.AddMoney(amount, false);
        }
    }
}
