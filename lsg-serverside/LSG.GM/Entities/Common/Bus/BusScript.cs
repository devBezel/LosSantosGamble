using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models.BusModels;
using LSG.GM.Constant;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Common.Bus
{
    public class BusScript : IScript
    {
        //public BusScript()
        //{
        //    Task.Run(() =>
        //    {
        //        AltAsync.OnColShape += OnColshape;
        //        AltAsync.OnClient("bus:selectStation", BusStationSelected);
        //        Alt.OnClient("bus:openWindow", BusOpenWindow);
        //    });

        //}
        [AsyncScriptEvent(ScriptEventType.ColShape)]
        public async Task OnColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;
            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;

            Alt.Log("Busy");

            BusEntity busEntity = colShape.GetBusEntity();

            if (busEntity == null || busEntity.ColShape != colShape) return;

            IPlayer player = targetEntity as IPlayer;

            if (player.IsInVehicle) return;

            new Interaction(player, "bus:openWindow", "aby otworzyć ~g~rozkład jazdy");
            player.SetData("current:bus", busEntity);
        });

        [ClientEvent("bus:openWindow")]
        public void BusOpenWindow(IPlayer player)
        {
            player.GetData("current:bus", out BusEntity busEntity);
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            player.Emit("bus:information", busEntity.DbModel, busEntity.DbModel.BusStopStations);
            player.DeleteData("current:bus");
        }
        [AsyncClientEvent("bus:selectStation")]
        public async Task BusStationSelected(IPlayer player, int id, int cost, float time, float posX, float posY, float posZ) => await AltAsync.Do(() =>
        {
            //int id = (int)(long)args[0];
            //int cost = (int)(long)args[1];
            //float time = (float)(long)args[2];
            //float posX = (float)(long)args[3];
            //float posY = (float)(long)args[4];
            //float posZ = (float)(long)args[5];

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if (!characterEntity.HasEnoughMoney(cost))
            {
                player.SendNativeNotify(null, NotificationNativeType.Bus, 1, "Kierowca autobusu", "~g~ Transakcja", $"Dałeś mi za mało gotówki, wypłać pieniądze i czekaj na następny autobus");
                return;
            }

            characterEntity.RemoveMoney(cost);
            player.SendNativeNotify(null, NotificationNativeType.Bus, 1, "Kierowca autobusu", "~g~ Transakcja", $"Zapłaciłeś {cost}$ za bilet");
            player.EmitAsync("bus:moneyRemovedStartTimer", time, posX, posY, posZ);

        });

        [Command("createbusstop")]
        public async Task CreateBusStopCMD(IPlayer sender) => await AltAsync.Do(async () =>
        {

            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            BusStop busStop = new BusStop()
            {
                PosX = sender.Position.X,
                PosY = sender.Position.Y,
                PosZ = sender.Position.Z,
                CreatorId = sender.GetAccountEntity().DbModel.Id
            };

            BusEntity busEntity = new BusEntity(busStop);
            await busEntity.Spawn(true);
        });

        [Command("createbusstaion")]
        public async Task CreateBusStationCMD(IPlayer sender, string name, int busStopId, int cost, float time) => await AltAsync.Do(async () =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            BusStopStation busStopStation = new BusStopStation()
            {
                BusStopId = busStopId,
                Name = name,
                PosX = sender.Position.X,
                PosY = sender.Position.Y,
                PosZ = sender.Position.Z,
                CreatorId = sender.GetAccountEntity().DbModel.Id,
                Cost = cost,
                Time = time
            };

            BusEntity bus = EntityHelper.GetById(busStopId);
            if (bus == null) return;

            await bus.CreateStation(busStopStation);
        });
    }
}
