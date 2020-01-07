using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleScript : IScript
    {
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
            vehicle.Spawn();

            player.SendSuccessNotify(null, $"Twój pojazd o ID {vehicle.DbModel.Id} został zespawniony");
        }

    }
}
