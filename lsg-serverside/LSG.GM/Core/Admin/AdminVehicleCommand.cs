using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Entities.Core.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using LSG.GM.Entities.Core;

namespace LSG.GM.Core.Admin
{
    public class AdminVehicleCommand : IScript
    {
        [Command("createveh")]
        public void CreateVehicleCMD(IPlayer player)
        {
            VehicleEntity.Create(player.Position, AltV.Net.Enums.VehicleModel.Infernus, "test", 1, new Color(), new Color(), player.GetCharacterEntity());
        }
    }
}
