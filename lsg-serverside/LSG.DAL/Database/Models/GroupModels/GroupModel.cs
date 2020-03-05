using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.GroupModels
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Grant { get; set; }
        public int MaxPayday { get; set; }

        public int Money { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [EnumDataType(typeof(GroupType))]
        public GroupType GroupType { get; set; }

        public int CreatorId { get; set; }
        public Character Creator { get; set; }

        public int LeaderId { get; set; }
        public Character Leader { get; set; }
    }
}
