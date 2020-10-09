using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Core.Player.Scripts
{
    public class AfkScript : IScript
    {
        [ClientEvent("player:afk")]
        public void PlayerAfkChecker(IPlayer player, bool IsAfk)
        {
            CharacterEntity character = player.GetAccountEntity().characterEntity;
            player.SetSyncedMetaData("player:afk", IsAfk);
            character.IsAfk = IsAfk;

            // Pobieranie rzeczy, które chcemy wstrzymać
            player.GetData("group:dutyTimer", out Timer dutyTimer);

            if(IsAfk)
            {
                // Wylączanie rzeczy podczas gdy ktoś jest afk

                if(dutyTimer != null)
                    dutyTimer.Stop();
                character.SpentTimer.Stop();
            } 
            else
            {
                if (dutyTimer != null)
                    dutyTimer.Start();
                character.SpentTimer.Start();
            }
        }
    }
}
