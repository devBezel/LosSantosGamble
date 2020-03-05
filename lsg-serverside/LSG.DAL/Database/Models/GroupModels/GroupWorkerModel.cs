using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.GroupModels
{
    public class GroupWorkerModel
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public int DutyMinutes { get; set; }
        [EnumDataType(typeof(GroupRights))]
        public GroupRights Rights { get; set; }

        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        //public int GroupRankId { get; set; }
        //public GroupRankModel GroupRank { get; set; }
    }
}
