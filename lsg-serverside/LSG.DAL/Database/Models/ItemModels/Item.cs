using AltV.Net;
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
    public class ItemModel : IWritable
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


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("creatorId");
            writer.Value(CreatorId.Value);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("firstParameter");
            writer.Value(FirstParameter.Value);

            writer.Name("secondParameter");
            writer.Value(SecondParameter.Value);

            writer.Name("thirdParameter");
            writer.Value(ThirdParameter.Value);

            writer.Name("fourthParameter");
            writer.Value(FourthParameter.Value);

            writer.Name("itemEntityType");
            writer.Value((int)ItemEntityType);

            writer.Name("characterId");
            writer.Value(CharacterId.Value);

            writer.Name("vehicleId");
            writer.Value(VehicleId.Value);

            writer.EndObject();

        }
    }
}
