using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtmModel = LSG.DAL.Database.Models.BankModels.Atm;

namespace LSG.GM.Entities.Common.Atm
{
    public class AtmScript :  IScript
    {

        public AtmScript()
        {
            Task.Run(() =>
            {
                AltAsync.OnColShape += OnColshape;
            }); 
        }

        private async Task OnColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;

            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;

            IPlayer player = targetEntity as IPlayer;

            if (player.IsInVehicle) return;

            AtmEntity atmEntity = colShape.GetAtmEntity();

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            player.SendSuccessNotify(null, $"Witaj w ATM {characterEntity.DbModel.Name}");
        });

        [Command("atm")]
        public void CreateAtmEntity(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            AtmModel atm = new AtmModel()
            {
                PosX = characterEntity.DbModel.PosX,
                PosY = characterEntity.DbModel.PosY,
                PosZ = characterEntity.DbModel.PosZ - 0.9f,
                CreatorId = player.GetAccountEntity().DbModel.Id
            };

            AtmEntity atmEntity = new AtmEntity(atm);
            atmEntity.Spawn();

        }
    }
}
