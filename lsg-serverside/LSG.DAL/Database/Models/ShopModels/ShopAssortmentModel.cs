using AltV.Net;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.ShopModels
{
    public class ShopAssortmentModel : IWritable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public double? FirstParameter { get; set; }
        public double? SecondParameter { get; set; }
        public double? ThirdParameter { get; set; }
        public double? FourthParameter { get; set; }

        [EnumDataType(typeof(ItemEntityType))]
        public ItemEntityType ItemEntityType { get; set; }

        public int ShopId { get; set; }
        public ShopModel Shop { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("count");
            writer.Value(Count);

            writer.Name("cost");
            writer.Value(Cost);

            if (FirstParameter.HasValue)
            {
                writer.Name("firstParameter");
                writer.Value(FirstParameter.Value);
            }

            if (SecondParameter.HasValue)
            {
                writer.Name("secondParameter");
                writer.Value(SecondParameter.Value);
            }

            if (ThirdParameter.HasValue)
            {
                writer.Name("thirdParameter");
                writer.Value(ThirdParameter.Value);
            }

            if (FourthParameter.HasValue)
            {
                writer.Name("fourthParameter");
                writer.Value(FourthParameter.Value);
            }


            writer.Name("itemEntityType");
            writer.Value((int)ItemEntityType);

            writer.Name("shopId");
            writer.Value(ShopId);



            writer.EndObject();

        }
    }
}
