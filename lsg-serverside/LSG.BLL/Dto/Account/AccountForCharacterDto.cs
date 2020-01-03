using AltV.Net;
using LSG.DAL.Database.Models.AccountModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Account
{
    public class AccountForCharacterDto : IWritable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Rank { get; set; }
        public AccountPremiumDto AccountPremium { get; set; }


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
