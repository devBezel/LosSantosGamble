using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Entities.Core.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using LSG.GM.Entities.Core;
using LSG.GM.Entities;
using System.Linq;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;
using LSG.GM.Extensions;

namespace LSG.GM.Core.Admin
{
    public class AdminVehicleCommand : IScript
    {
        [Command("createveh")]
        public void CreateVehicleCMD(IPlayer player)
        {
            VehicleEntity vehicle = VehicleEntity.Create(player.Position, AltV.Net.Enums.VehicleModel.Infernus, "test", 1, new Color(), new Color(), player.GetAccountEntity().characterEntity.DbModel);
            vehicle.Spawn(player);
        }


        [Command("tptoveh")]
        public void TeleportToVehicleCMD(IPlayer player, int id)
        {
            VehicleEntity vehicleEntity = EntityHelper.GetSpawnedVehicleById(id);
            if (vehicleEntity == null)
            {
                player.SendErrorNotify(null, $"Pojazd o ID {vehicleEntity.DbModel.Id} jest odspawniony");

                return;
            }
            
            player.Position = vehicleEntity.GameVehicle.Position;
            player.SendSuccessNotify(null, $"Przeteleportowałeś się do pojazdu o ID: {vehicleEntity.DbModel.Id}");
        }
    }
}
