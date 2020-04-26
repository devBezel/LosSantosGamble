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
                    CharacterEntity character = plr.GetAccountEntity().characterEntity;
                    if (character == null)
                        return;
                    playerList.Add(new ScoreboardModel { Id = character.AccountEntity.ServerID, FormatName = $"{character.DbModel.Name} {character.DbModel.Surname}", GamblePoints = character.DbModel.GamblePoints, Ping = plr.Ping });
                }
            });

            await player.EmitAsync("scoreboard:data", playerList);
        }
    }
}
