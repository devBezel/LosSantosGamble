using LSG.DAL.Database.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.BankModels
{
    public class Atm
    {
        public int Id { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public int CreatorId { get; set; }

        public virtual Account Creator { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
