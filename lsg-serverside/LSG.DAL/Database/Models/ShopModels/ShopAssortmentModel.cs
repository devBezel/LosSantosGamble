using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.ShopModels
{
    public class ShopAssortmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double? FirstParameter { get; set; }
        public double? SecondParameter { get; set; }
        public double? ThirdParameter { get; set; }
        public double? FourthParameter { get; set; }

        [EnumDataType(typeof(ItemEntityType))]
        public ItemEntityType ItemEntityType { get; set; }

        public int ShopId { get; set; }
        public ShopModel Shop { get; set; }
    }
}
