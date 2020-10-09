using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Core.Scoreboard
{
    public class ScoreboardScript : IScript
    {
        // WAŻNE: Może pobierać duże zasoby serwera, sprawdzić jeśli będą lagi w pierwszej kolejności
        [AsyncClientEvent("scoreboard:fetchPlayers")]
        public async Task ScoreboardFetchPlayers(IPlayer player)
        {
            List<ScoreboardModel> playerList = new List<ScoreboardModel>();
            await AltAsync.Do(() =>
            {
                foreach (IPlayer plr in Alt.GetAllPlayers())
                {
                    AccountEntity account = plr.GetAccountEntity();
                    if (account == null || account.characterEntity == null)
                    {
                        playerList.Add(new ScoreboardModel { Id = -1, FormatName = $"Niezalogowany {plr.Name}", GamblePoints = 0, Ping = plr.Ping });
                    }
                    else
                    {
                        playerList.Add(new ScoreboardModel { Id = account.characterEntity.AccountEntity.ServerID, FormatName = $"{account.characterEntity.DbModel.Name} " +
                                                           $"{account.characterEntity.DbModel.Surname}", GamblePoints = account.characterEntity.DbModel.GamblePoints, Ping = plr.Ping });
                    }
                    
                }
            });

            await player.EmitAsync("scoreboard:data", playerList);
        }
    }
}
