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
        }

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
                spawnedVehicle.Dispose();

                player.SendSuccessNotify(null, $"Twój pojazd o ID {spawnedVehicle.DbModel.Id} został odspawniony");

                return;
            }

            VehicleEntity vehicle = new VehicleEntity(EntityHelper.GetVehicleDatabaseById(vehicleId));
            vehicle.Spawn(player);

            Alt.Log(vehicle.IncrementID.ToString() + " IncrementID");
            Alt.Log(player.GetAccountEntity().HasPremium + " HasPremium");

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
