using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.DAL.Database.Models;
using Newtonsoft.Json.Linq;
using LSG.DAL.Database;
using LSG.DAL.Repositories;
using AltV.Net.Data;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.BLL.Dto.Character;
using AltV.Net;
using LSG.GM.Entities.Core;
using LSG.DAL.UnitOfWork;
using LSG.GM.Extensions;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Utilities;

namespace LSG.GM.Core.Login
{
    public class LoginScript : IScript
    {
        public LoginScript()
        {
            Task.Run(() =>
            {
                AltAsync.OnPlayerConnect += OnPlayerConnect;
            });

            AltAsync.OnClient("login:characterDetail", SetCharacterSettings);
        }

        private async Task SetCharacterSettings(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            
            Character character = JsonConvert.DeserializeObject<Character>((string)args[0]);
            player.SetData("account:data", character.Account);
            player.SetData("character:data", character);
            player.SetData("account:id", Calculation.GenerateFreeIdentifier());

            player.Spawn(new Position(character.PosX, character.PosY, character.PosZ));
            player.SetHealthAsync((ushort)character.Health);
            player.SetModelAsync(0x705E61F2);
            player.SetNameAsync(character.Name);
            player.SendAccountDataToClient();
            player.SendCharacterDataToClient();

            if(character.Gender)
            {
                player.SetModelAsync(0x9C9EFFD8);
            }

            if (player.HasPremium())
                player.SendChatMessage("Dziękujemy za wspieranie naszego projektu " + character.Account.Username + "! Do końca twojego {D1BA0f} premium {ffffff} pozostało " +
                        Calculation.CalculateTheNumberOfDays(character.Account.AccountPremium.EndTime, character.Account.AccountPremium.BoughtTime) + " dni");

            player.EmitAsync("character:wearClothes", character.CharacterLook);
        });
        private async Task OnPlayerConnect(IPlayer player, string reason) => await AltAsync.Do(() =>
        {
            player.EmitAsync("other:first-connect");
        });
        [Command("test")]
        public void TestCMD(IPlayer player, int rank)
        {
            Alt.Log(rank.ToString());
            if (player.HasRank(rank))
                player.SendChatMessage("Posiadasz rangę");
            else
                player.SendChatMessage("Nie posiadasz rangi");
        }

    }
}
