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

namespace LSG.GM.Core.Login
{
    public class LoginScript 
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
            player.EmitAsync("character:wearClothes", character.CharacterLook);

        });

        private async Task OnPlayerConnect(IPlayer player, string reason) => await AltAsync.Do(() =>
        {
            player.EmitAsync("other:first-connect");
        });

    }
}
