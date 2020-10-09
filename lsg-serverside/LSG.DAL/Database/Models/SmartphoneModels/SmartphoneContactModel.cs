using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.SmartphoneModels
{
    public class SmartphoneContactModel : IWritable
    {
        public int Id { get; set; }
        public int PhoneItemId { get; set; }
        public ItemModel PhoneItem { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Notes { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsAlarmNumber { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("phoneItemId");
            writer.Value(PhoneItemId);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("number");
            writer.Value(Number);

            writer.Name("notes");
            writer.Value(Notes);


            writer.Name("isFavorite");
            writer.Value(IsFavorite);

            writer.Name("isAlarmNumber");
            writer.Value(IsAlarmNumber);


            writer.EndObject();

        }
    }
}
