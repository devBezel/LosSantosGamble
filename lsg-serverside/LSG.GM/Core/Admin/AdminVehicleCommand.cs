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
using LSG.GM.Enums;
using AltV.Net.Enums;

namespace LSG.GM.Core.Admin
{
    public class AdminVehicleCommand : IScript
    {
        [Command("createveh")]
        public void CreateGlobalVehicleCMD(IPlayer sender, int id, VehicleModel model)
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.CommunityManager))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);
            if (getter == null)
            {
                sender.SendErrorNotify(null, $"Gracz o ID {id} nie jest w grze");
            }

            if (model == 0) return;

            VehicleEntity vehicle = VehicleEntity.Create(sender.Position, model, new Color(), new Color(), getter.GetAccountEntity().characterEntity.DbModel);
            vehicle.Spawn();
        }


        [Command("vtp")]
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

        [Command("atuning")]
        public void AdminTuningVehicleTemporary(IPlayer player, int category, int index)
        {
            IVehicle vehicle = player.Vehicle;
            if (vehicle == null)
                return;

            vehicle.ModKit = 1;
            vehicle.SetMod((byte)category, (byte)index);
        }
    }
}
