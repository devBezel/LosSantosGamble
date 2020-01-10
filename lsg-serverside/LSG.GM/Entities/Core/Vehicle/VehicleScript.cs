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
            AltAsync.OnClient("vehicle:spawnVehicle", SpawnOwnVehicle);
        }

        [Command("vc")]
        public void SpawnOwnVehicleWithoutCefCMD(IPlayer player, int id)
        {
            VehicleEntity spawnedVehicle = EntityHelper.GetSpawnedVehicleById(id);

            if (spawnedVehicle != null)
            {
                spawnedVehicle.Dispose();

                player.SendSuccessNotify(null, $"Twój pojazd o ID {spawnedVehicle.DbModel.Id} został odspawniony");

                return;
            }

            VehicleEntity vehicle = new VehicleEntity(EntityHelper.GetVehicleDatabaseById(id));
            vehicle.Spawn(player);



            if (vehicle.GetIncrementID() >= 3 && !player.GetAccountEntity().HasPremium || !player.GetAccountEntity().OnAdminDuty)
            {
                player.SendErrorNotify(null, $"Aby zrespić więcej niż 3 pojazdy musisz posiadać premium");
                vehicle.Dispose();
                return;
            }

            player.SendSuccessNotify(null, $"Twój pojazd o ID {vehicle.DbModel.Id} został zespawniony");
        }

        [Command("v")]
        public async Task OpenVehicleCEFWindowCMD(IPlayer player) => await AltAsync.Do(() =>
        {
            player.EmitAsync("vehicle:openWindow", EntityHelper.GetCharacterVehicleDatabaseList(player.GetAccountEntity().characterEntity.DbModel.Id));
        });

        public async Task SpawnOwnVehicle(IPlayer player, object[] args) => await AltAsync.Do(() =>
        {
            VehicleDb vehicleModel = JsonConvert.DeserializeObject<VehicleDb>((string)args[0]);

            Alt.Log(vehicleModel.Id.ToString());
            VehicleEntity spawnedVehicle = EntityHelper.GetSpawnedVehicleById(vehicleModel.Id);

            Alt.Log(spawnedVehicle.GameVehicle.HealthData);
            if (spawnedVehicle != null)
            {
                spawnedVehicle.Dispose();

                player.SendSuccessNotify(null, $"Twój pojazd o ID {spawnedVehicle.DbModel.Id} został odspawniony");

                return;
            }

            VehicleEntity vehicle = new VehicleEntity(EntityHelper.GetVehicleDatabaseById(vehicleModel.Id));
            vehicle.Spawn(player);



            if (vehicle.GetIncrementID() >= 3 && !player.GetAccountEntity().HasPremium || !player.GetAccountEntity().OnAdminDuty)
            {
                player.SendErrorNotify(null, $"Aby zrespić więcej niż 3 pojazdy musisz posiadać premium");
                vehicle.Dispose();
                return;
            }

            player.SendSuccessNotify(null, $"Twój pojazd o ID {vehicle.DbModel.Id} został zespawniony");

        });
    }
}
