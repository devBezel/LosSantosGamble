using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Dto.Vehicle;
//using LSG.GM.Entities.Admin;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleScript : IScript
    {
        public VehicleScript()
        {
            Alt.OnClient("vehicle:spawnVehicle", SpawnOwnVehicle);
            Task.Run(() =>
            {
                AltAsync.OnPlayerLeaveVehicle += OnPlayerLeaveVehicle;
            });

            AltAsync.OnPlayerEnterVehicle += OnPlayerEnterVehicle;
        }

        private async Task OnPlayerEnterVehicle(IVehicle vehicle, IPlayer player, byte seat) => await AltAsync.Do(() =>
        {
            player.EmitAsync("player:enterVehicle", (int)seat);
        });

        private async Task OnPlayerLeaveVehicle(IVehicle vehicle, IPlayer player, byte seat) => await AltAsync.Do(() =>
        {
            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();
            if (seat == 1)
            {
                if (vehicleEntity == null) return;

                vehicleEntity.Save();
            }
        });

        [Command("v")]
        public async Task OpenVehicleCEFWindowCMD(IPlayer player) => await AltAsync.Do(() =>
        {
            player.EmitAsync("vehicle:openWindow", EntityHelper.GetCharacterVehicleDatabaseList(player.GetAccountEntity().characterEntity.DbModel.Id));
        });

        public void SpawnOwnVehicle(IPlayer player, object[] args)
        {
            int vehicleId = (int)(long)args[0];
   
            VehicleEntity spawnedVehicle = EntityHelper.GetSpawnedVehicleById(vehicleId);
            if (spawnedVehicle != null)
            {
                if(player.Vehicle == spawnedVehicle.GameVehicle)
                {
                    player.SendErrorNotify(null, $"Aby odpspawnić pojazd musisz z niego wyjść");
                    return;
                }

                spawnedVehicle.Dispose();

                player.SendSuccessNotify(null, $"Twój pojazd o ID {spawnedVehicle.DbModel.Id} został odspawniony");

                return;
            }

            VehicleEntity vehicle = new VehicleEntity(EntityHelper.GetVehicleDatabaseById(vehicleId));
            vehicle.Spawn(player);


            if (vehicle.IncrementID > 3 && !player.GetAccountEntity().HasPremium)
            {
                if (player.GetAccountEntity().OnAdminDuty) return;

                player.SendErrorNotify(null, $"Aby zrespić więcej niż 3 pojazdy musisz posiadać premium");
                vehicle.Dispose();
                return;
            }

            player.SendSuccessNotify(null, $"Twój pojazd o ID {vehicle.DbModel.Id} został zespawniony");

        }
    }
}
