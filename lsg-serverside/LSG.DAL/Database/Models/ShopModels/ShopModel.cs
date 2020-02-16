using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.ShopModels
{
    public class ShopModel
    {
        public int Id { get; set; }
        [EnumDataType(typeof(ItemEntityType))]
        public ShopEntityType ShopEntityType { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public List<ShopAssortmentModel> ShopAssortments { get; set; }
    }
}
