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
        [Command("test")]
        public void TestCommand(IPlayer player)
        {
            player.GetData("character-data", out Character character);

            player.SendChatMessage($"Witaj {character.Name}");
        }
    }
}
