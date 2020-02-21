using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleTrunkScript : IScript
    {
        public VehicleTrunkScript()
        {
            AltAsync.OnClient("vehicle-interaction:openTrunkRequest", OpenVehicleTrunkRequest);
            AltAsync.OnColShape += OnEnterColshape;
            Alt.OnClient("vehicle-trunk:open", OpenVehicleTrunk);
        }

        private async Task OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            IPlayer player = targetEntity as IPlayer;
            if (!state) return;


            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;
            Alt.Log("Bagażniki");

            if (!colShape.HasData("vehicle:trunk")) return;
            Alt.Log("Wchodze w bagaznik colshape");
            new Interaction(player, "vehicle-trunk:open", "aby otworzyć ~g~bagażnik");
            player.SetData("current:vehicle-trunk", colShape);

        });

        public async Task OpenVehicleTrunkRequest(IPlayer player, object[] args) => await AltAsync.Do(async () =>
        {
            Alt.Log($"event dotarł trunk {args[0].ToString()}");
            IVehicle vehicle = (IVehicle)args[0];
            Alt.Log($"{vehicle.Id}");
            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();
            Alt.Log($"vehicleEntity: {vehicleEntity.DbModel.Model}");

            if (vehicleEntity == null) return;



            IColShape trunkColShape = Alt.CreateColShapeCylinder(new Position(vehicle.Position.X, vehicle.Position.Y, vehicle.Position.Z), 1f, 2f);
            trunkColShape.SetData("vehicle:trunk", vehicleEntity);

            Alt.Log("Wykonala sie dalsza czesc kodu trank");

            MarkerModel marker = new MarkerModel()
            {
                Type = 27,
                Dimension = 0,
                PosX = vehicle.Position.X,
                PosY = vehicle.Position.Y,
                PosZ = vehicle.Position.Z,
                DirX = 0,
                DirY = 0,
                DirZ = 0,
                RotX = 0,
                RotY = 0,
                RotZ = 0,
                ScaleX = 1f,
                ScaleY = 1f,
                ScaleZ = 1f,
                Red = 0,
                Green = 153,
                Blue = 0,
                Alpha = 100,
                BobUpAndDown = false,
                FaceCamera = false,
                P19 = 2,
                Rotate = false,
                TextureDict = null,
                TextureName = null,
                DrawOnEnts = false,
                UniqueID = $"VEHICLE_TRUNK_MARKER{vehicleEntity.DbModel.Id}"
            };

            await MarkerHelper.CreateGlobalMarker(marker);
        });

        public void OpenVehicleTrunk(IPlayer player, object[] args)
        {
            player.GetData("current:vehicle-trunk", out IColShape trunkColshape);
            if (trunkColshape == null) return;

            trunkColshape.GetData("vehicle:trunk", out VehicleEntity vehicleEntity);
            if (vehicleEntity == null) return;

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if(vehicleEntity.DbModel.OwnerId != characterEntity.DbModel.Id)
            {
                //TODO: Dorobić że typ grupy police może przeszukiwać bagażniki
                player.SendErrorNotify("Nie masz uprawnień", "Nie jesteś właścicielem tego pojazdu");
                return;
            }

            player.Emit("vehicle-trunk:data",
                characterEntity.DbModel.Items,
                vehicleEntity.DbModel.ItemsInVehicle);
        }
    }
}
