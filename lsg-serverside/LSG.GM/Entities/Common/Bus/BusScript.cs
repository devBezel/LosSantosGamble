using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models.BusModels;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Common.Bus
{
    public class BusScript : IScript
    {
        public BusScript()
        {
            Task.Run(() =>
            {
                AltAsync.OnColShape += OnColshape;
                AltAsync.OnClient("bus:selectStation", BusStationSelected);
            });

        }

        private async Task OnColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;
            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;

            Alt.Log("Busy");

            BusEntity busEntity = colShape.GetBusEntity();

            if (busEntity == null || busEntity.ColShape != colShape) return;

            IPlayer player = targetEntity as IPlayer;

            if (player.IsInVehicle) return;


            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            player.EmitAsync("bus:information", busEntity.DbModel, busEntity.DbModel.BusStopStations);
        });

        private async Task BusStationSelected(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            int id = (int)(long)args[0];
            int cost = (int)(long)args[1];
            float time = (float)(long)args[2];
            float posX = (float)(long)args[3];
            float posY = (float)(long)args[4];
            float posZ = (float)(long)args[5];

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if (!characterEntity.HasEnoughMoney(cost))
            {
                player.SendErrorNotify("", "Nie masz wystarczająco pieniędzy");
                return;
            }

            characterEntity.RemoveMoney(cost);
            player.SendSuccessNotify(null, $"Zapłaciłeś {cost}$ za bilet");
            player.EmitAsync("bus:moneyRemovedStartTimer", time, posX, posY, posZ);

        });

        [Command("createbusstop")]
        public async Task CreateBusStopCMD(IPlayer player) => await AltAsync.Do(async () =>
        {
            BusStop busStop = new BusStop()
            {
                PosX = player.Position.X,
                PosY = player.Position.Y,
                PosZ = player.Position.Z,
                CreatorId = player.GetAccountEntity().DbModel.Id
            };

            BusEntity busEntity = new BusEntity(busStop);
            await busEntity.Spawn(true);
        });

        [Command("createbusstaion")]
        public async Task CreateBusStationCMD(IPlayer player, string name, int busStopId, int cost, float time) => await AltAsync.Do(async () =>
        {
            BusStopStation busStopStation = new BusStopStation()
            {
                BusStopId = busStopId,
                Name = name,
                PosX = player.Position.X,
                PosY = player.Position.Y,
                PosZ = player.Position.Z,
                CreatorId = player.GetAccountEntity().DbModel.Id,
                Cost = cost,
                Time = time
            };

            BusEntity bus = EntityHelper.GetById(busStopId);
            if (bus == null) return;

            await bus.CreateStation(busStopStation);
        });
    }
}
