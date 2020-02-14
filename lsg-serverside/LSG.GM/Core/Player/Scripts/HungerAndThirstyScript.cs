using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Core.Player.Scripts
{
    public class HungerAndThirstyScript : IScript
    {
        public HungerAndThirstyScript()
        {
            AltAsync.OnClient("hungerThirsty:subtract", SubtractHungerAndThirsty);
        }

        private async Task SubtractHungerAndThirsty(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity.Hunger == 0 || characterEntity.Thirsty == 0) return;

            AltAsync.Log("Dotarł event z jedzeniem");

            characterEntity.Hunger -= 3.0f;
            characterEntity.Thirsty -= 3.0f;
        });
    }
}
