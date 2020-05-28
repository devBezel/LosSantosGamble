using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.SmartphoneModels
{
    public class SmartphoneMessageModel : IWritable
    {
        public int Id { get; set; }
        public int GetterNumber { get; set; }

        public int CellphoneId { get; set; }
        public ItemModel Cellphone { get; set; }

        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("getterNumber");
            writer.Value(GetterNumber);

            writer.Name("cellphoneId");
            writer.Value(CellphoneId);

            writer.Name("cellphone");
            Cellphone.OnWrite(writer);

            writer.Name("message");
            writer.Value(Message);

            writer.Name("date");
            writer.Value(Date.ToString());


            writer.Name("isRead");
            writer.Value(IsRead);


            writer.EndObject();

        }
    }
}
