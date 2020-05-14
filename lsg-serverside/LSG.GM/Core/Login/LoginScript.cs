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
using LSG.GM.Entities;

namespace LSG.GM.Core.Login
{
    public class LoginScript : IScript
    {
        //public LoginScript()
        //{
        //    Task.Run(() =>
        //    {
        //        AltAsync.OnPlayerConnect += OnPlayerConnect;
        //        AltAsync.OnClient("login:characterDetail", SetCharacterSettings);
        //    });

        //    Alt.OnClient("login:successWearChangeWorld", ChangeCharacterWorld);
        //}


        [AsyncClientEvent("login:characterDetail")]
        public void SetCharacterSettings(IPlayer player, int characterId)
        {
            //Character characterClient = JsonConvert.DeserializeObject<Character>((string)args[0]);


            Character characterDatabase = Singleton.GetDatabaseInstance().Characters
            .Include(l => l.CharacterLook)
            .Include(g => g.GroupWorkers)
            .Include(i => i.Items)
            .Include(v => v.Vehicles)
            .Include(a => a.Account)
            .ThenInclude(p => p.AccountPremium)
            .FirstOrDefault(c => c.Id == characterId);

            
            if(EntityHelper.AccountLogged(characterDatabase.Account.Id))
            {
                player.SendChatMessageError("Ktoś już jest zalogowany na tym koncie!");
                return;
            }

            AccountEntity accountEntity = new AccountEntity(characterDatabase.Account, player);
            accountEntity.Login(characterDatabase);
        }
        
        [ClientEvent("login:successWearChangeWorld")]
        public void ChangeCharacterWorld(IPlayer player)
        {
            // Ustawianie domyślnego świata po wyborze postaci
            Alt.Log("Zmieniam świat");
            player.Dimension = 0;
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(IPlayer player, string reason)
        {
            if (player == null || player.GetAccountEntity() == null) return;
            player.GetAccountEntity().Dispose();
        }



        [AsyncScriptEvent(ScriptEventType.PlayerConnect)]
        public async Task OnPlayerConnect(IPlayer player, string reason)
        {
            await EntityHelper.LoadClientEntity(player);
            Calculation.AssignPlayerServerID(player);


            await player.EmitAsync("other:first-connect");
        }


    }
}
