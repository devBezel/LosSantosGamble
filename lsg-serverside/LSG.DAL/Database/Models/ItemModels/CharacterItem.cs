using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.VehicleModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.ItemModels
{
    public class CharacterItem
    {
        public int Id { get; set; }
        public int? CreatorId { get; set; }
        public Account Creator { get; set; }
        public string Name { get; set; }
        public double? FirstParameter { get; set; }
        public double? SecondParameter { get; set; }
        public double? ThirdParameter { get; set; }
        public double? FourthParameter { get; set; }

        [EnumDataType(typeof(ItemEntityType))]
        public ItemEntityType ItemEntityType { get; set; }

        public int? CharacterId { get; set; }
        public Character Character { get; set; }
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
