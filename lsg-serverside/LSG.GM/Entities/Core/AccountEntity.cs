using AltV.Net.Elements.Entities;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core
{
    public static class AccountEntity
    {
        
        public static bool HasRank(this IPlayer player, int rank)
        {
            player.GetData("account-data", out AccountForCharacterDto account);

            return account.Rank >= rank ? true : false;
        }

        public static bool HasPremium(this IPlayer player)
        {
            player.GetData("account-data", out AccountForCharacterDto account);

            return ((account.AccountPremium != null) && (account.AccountPremium.EndTime >= DateTime.Now)) ? true : false;
        }
    }
}
