using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Account
{
    public class AccountPremiumDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime BoughtTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
    }
}
