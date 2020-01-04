using AltV.Net.Elements.Entities;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core
{
    public static class AccountEntity
    {

        public static Account GetAccountEntity(this IPlayer player)
        {
            player.GetData("account:data", out Account account);

            return account;
        }
        
        public static bool HasRank(this IPlayer player, int rank)
        {
            player.GetData("account:data", out Account account);

            return account.Rank >= rank ? true : false;
        }

        public static bool HasPremium(this IPlayer player)
        {
            player.GetData("account:data", out Account account);

            if (((account.AccountPremium == null) || (account.AccountPremium.EndTime <= DateTime.Now)))
            {
                player.Emit("account:hasPremium", false);
                player.SetData("account:premium", false);

                return false;
            }

            player.Emit("account:hasPremium", true);
            player.SetData("account:premium", true);
            return true;
        }

        public static void SendAccountDataToClient(this IPlayer player)
        {
            player.GetData("account:data", out Account account);
            player.GetData("account:id", out int id);
            player.Emit("account:sendDataAccount", account, id);
        }

        public static void SendCharacterDataToClient(this IPlayer player)
        {
            player.GetData("character:data", out Character character);
            player.Emit("character:sendDataCharacter", character);
        }

        public static void UpdateAccountData(this IPlayer player, Account account)
        {
            player.SetData("account:data", account);
            player.SendAccountDataToClient();
        }
    }
}
