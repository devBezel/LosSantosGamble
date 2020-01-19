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
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LSG.GM.Core.Login
{
    public class LoginScript : IScript
    {
        public LoginScript()
        {
            Task.Run(() =>
            {
                AltAsync.OnPlayerConnect += OnPlayerConnect;
                AltAsync.OnClient("login:characterDetail", SetCharacterSettings);
            });

            Alt.OnClient("login:successWearChangeWorld", ChangeCharacterWorld);
        }

        private async Task SetCharacterSettings(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            
            Character characterClient = JsonConvert.DeserializeObject<Character>((string)args[0]);
            Character characterDatabase = Singleton.GetDatabaseInstance().Characters
            .Include(l => l.CharacterLook)
            .Include(a => a.Account)
            .ThenInclude(p => p.AccountPremium)
            .FirstOrDefault(c => c.Id == characterClient.Id);


            AccountEntity accountEntity = new AccountEntity(characterDatabase.Account, player);
            accountEntity.Login(characterDatabase);
        });

        private void ChangeCharacterWorld(IPlayer player, object[] args)
        {
            // Ustawianie domyślnego świata po wyborze postaci
            Alt.Log("Zmieniam świat");
            player.Dimension = 0;
        }

        private async Task OnPlayerConnect(IPlayer player, string reason) => await AltAsync.Do(() =>
        {
            player.EmitAsync("other:first-connect");
        });


    }
}
