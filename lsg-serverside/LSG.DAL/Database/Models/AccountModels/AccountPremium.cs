using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.AccountModels
{
    public class AccountPremium
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime BoughtTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
    }
}
