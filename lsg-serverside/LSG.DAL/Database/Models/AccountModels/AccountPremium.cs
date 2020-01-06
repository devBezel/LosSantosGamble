using AltV.Net;
using LSG.DAL.Database.Models.CharacterModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.AccountModels
{
    public class AccountPremium : IWritable
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        public DateTime BoughtTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("accountId");
            writer.Value(AccountId);

            writer.Name("account");
            writer.Value(JsonConvert.SerializeObject(Account));

            writer.Name("boughtTime");
            writer.Value(JsonConvert.SerializeObject(BoughtTime));

            writer.Name("endTime");
            writer.Value(JsonConvert.SerializeObject(EndTime));

            writer.EndObject();

        }
    }
}
