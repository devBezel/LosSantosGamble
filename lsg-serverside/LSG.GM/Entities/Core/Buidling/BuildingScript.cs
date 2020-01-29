using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Constant;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Buidling
{
    public class BuildingScript : IScript
    {
        public BuildingScript()
        {
            AltAsync.OnColShape += OnEnterColshape;
            Alt.OnClient("building:enterBuilding", OnEnterBuilding);
            Alt.OnClient("building:exitBuilding", OnExitBuilding);
        }

        private async Task OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;

            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;
            Alt.Log("Budynki");

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();

            Alt.Log("Pozycja exterioru: "  + buildingEntity.ExteriorColshape.Position.ToString());

            if (buildingEntity == null) return;

            IPlayer player = targetEntity as IPlayer;

            Alt.Log("Przeszedł ifa ze sprawdzaniem colshapeow");
            // Wejście do budynku
            if(buildingEntity.InteriorColshape == colShape)
            {
                player.EmitAsync("building:request", buildingEntity.DbModel.EntryFee, buildingEntity.DbModel.Name, true);
                player.SetData("current:doors", colShape);

            } else if (buildingEntity.ExteriorColshape == colShape)
            {
                player.EmitAsync("building:request", buildingEntity.DbModel.EntryFee, buildingEntity.DbModel.Name, false);
            }
        });

        public void OnEnterBuilding(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();

            player.Dimension = buildingEntity.DbModel.Id;
            player.Position = new Position(buildingEntity.DbModel.ExternalPickupPositionX, buildingEntity.DbModel.ExternalPickupPositionY, buildingEntity.DbModel.ExternalPickupPositionZ);

            if(buildingEntity.DbModel.EntryFee > 0)
            {
                player.GetAccountEntity().characterEntity.RemoveMoney((int)buildingEntity.DbModel.EntryFee);
                player.SendNativeNotify(null, NotificationNativeType.Normal, 1, "Pobrano opłatę", "~g~ Budynek", $"Pobrano opłatę {buildingEntity.DbModel.EntryFee}$ za wejście do budynku", 1);
            }

            // W późniejszym czasie wczytywanie customowych obiektów - dorobić tabelę building_objects i tam umieszczać obiekty

            buildingEntity.PlayersInBuilding.Add(player);
        }

        public void OnExitBuilding(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();


            player.Dimension = 0; // Defaultowy świat graczy
            player.Position = new Position(buildingEntity.DbModel.InternalPickupPositionX, buildingEntity.DbModel.InternalPickupPositionY, buildingEntity.DbModel.InternalPickupPositionZ);

            buildingEntity.PlayersInBuilding.Remove(player);

            player.DeleteData("current:doors");
        }
    }
}
