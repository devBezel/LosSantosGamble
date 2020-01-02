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
            AltAsync.Log("Działa");
        }

        private async Task SetCharacterSettings(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            
            CharacterForListDto character = JsonConvert.DeserializeObject<CharacterForListDto>((string)args[0]);
            player.SetData("account-data", character.Account);
            player.SetData("character-data", character);

            player.Spawn(new Position(character.PosX, character.PosY, character.PosZ));
            player.SetHealthAsync((ushort)character.Health);
            player.SetModelAsync(0x705E61F2);

            if(character.Gender)
            {
                player.SetModelAsync(0x9C9EFFD8);
            }

            if (player.HasPremium())
                player.SetSyncedMetaDataAsync("account-premium", true);

            player.SendChatMessage("Dziękujemy za wspieranie naszego projektu! Do końca twojego premium pozostało " + 
                Calculation.CalculateTheNumberOfDays(character.Account.AccountPremium.BoughtTime, character.Account.AccountPremium.EndTime) + " Dni");
            
            player.EmitAsync("character:wearClothes", character.CharacterLook);
        });
        private async Task OnPlayerConnect(IPlayer player, string reason) => await AltAsync.Do(() =>
        {
            player.EmitAsync("other:first-connect");
        });

        [Command("test")]
        public void TestCMD(IPlayer player)
        {
            Alt.Log("dziala");
            player.SendSuccessNotify("Witaj na serwerze ", "Baw się dobrze i nie łam regulaminu!");
        }

    }
}
