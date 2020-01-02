using LSG.DAL.Database.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Account
{
    public class AccountForCharacterDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Rank { get; set; }
        public AccountPremiumDto AccountPremium { get; set; }
    }
}
