using AltV.Net;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.VehicleModels;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.ItemModels
{
    public class ItemModel : IWritable
    {
        public int Id { get; set; }
        public int? CreatorId { get; set; } 
        public Account Creator { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
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

        public int? BuildingId { get; set; }
        public BuildingModel Building { get; set; }

        public int? WarehouseId { get; set; }
        public WarehouseModel Warehouse { get; set; }

        public int? VehicleUpgradeId { get; set; }
        public Vehicle VehicleUpgrade { get; set; }

        public bool ItemInUse { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            if(CreatorId.HasValue)
            {
                writer.Name("creatorId");
                writer.Value(CreatorId.Value);
            }

            writer.Name("name");
            writer.Value(Name);

            writer.Name("count");
            writer.Value(Count);

            if (FirstParameter.HasValue)
            {
                writer.Name("firstParameter");
                writer.Value(FirstParameter.Value);
            }

            if(SecondParameter.HasValue)
            {
                writer.Name("secondParameter");
                writer.Value(SecondParameter.Value);
            }

            if(ThirdParameter.HasValue)
            {
                writer.Name("thirdParameter");
                writer.Value(ThirdParameter.Value);
            }

            if(FourthParameter.HasValue)
            {
                writer.Name("fourthParameter");
                writer.Value(FourthParameter.Value);
            }
           

            writer.Name("itemEntityType");
            writer.Value((int)ItemEntityType);
            if(CharacterId.HasValue)
            {
                writer.Name("characterId");
                writer.Value(CharacterId.Value);
            }

            if(VehicleId.HasValue)
            {
                writer.Name("vehicleId");
                writer.Value(VehicleId.Value);
            }

            if(BuildingId.HasValue)
            {
                writer.Name("buildingId");
                writer.Value(BuildingId.Value);
            }

            if(WarehouseId.HasValue)
            {
                writer.Name("warehouseId");
                writer.Value(WarehouseId.Value);
            }

            if(VehicleUpgradeId.HasValue)
            {
                writer.Name("vehicleUpgradeId");
                writer.Value(VehicleUpgradeId.Value);
            }

            writer.Name("itemInUse");
            writer.Value(ItemInUse);


            writer.EndObject();

        }
    }
}
