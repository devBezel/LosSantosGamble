using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.SmartphoneModels
{
    public class SmartphoneRecentCallModel : IWritable
    {
        public int Id { get; set; }
        public int PhoneItemId { get; set; }
        public ItemModel PhoneItem { get; set; }
        public int CallNumber { get; set; }
        public DateTime CalledDate { get; set; }
        public int CallTime { get; set; }
        public bool IsAnwser { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("phoneItemId");
            writer.Value(PhoneItemId);

            writer.Name("callNumber");
            writer.Value(CallNumber);

            writer.Name("calledDate");
            writer.Value(CalledDate.ToString());

            writer.Name("callTime");
            writer.Value(CallTime);


            writer.Name("isAnwser");
            writer.Value(IsAnwser);


            writer.EndObject();

        }
    }
}
