using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core.Description
{
    public class DescriptionScript : IScript
    {
        public DescriptionScript()
        {
            Alt.OnClient("character:setDescription", SetCharacterDescritpion);
        }

        [Command("opis")]
        public void GetCharacterDescriptionCMD(IPlayer player)
        {
            player.Emit("description:getCharacterDescription");
        }

        private void SetCharacterDescritpion(IPlayer player, object[] args)
        {
            string contentDescription = (string)args[0];

            player.SetSyncedMetaData("character:description", contentDescription);
        }
    }
}
