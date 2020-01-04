using AltV.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.AccountModels
{
    public class Account : IWritable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Rank { get; set; }
        public virtual AccountPremium AccountPremium { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("username");
            writer.Value(Username);

            writer.Name("rank");
            writer.Value(Rank);

            writer.Name("accountPremium");
            writer.Value(JsonConvert.SerializeObject(AccountPremium));

            writer.EndObject();

        }
    }
}
