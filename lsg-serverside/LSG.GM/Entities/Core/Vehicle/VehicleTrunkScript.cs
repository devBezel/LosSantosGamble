using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            AltAsync.OnPlayerEnterVehicle += OnEnterPlayerVehicle;
            Alt.OnClient("vehicle-trunk:open", OpenVehicleTrunk);
            AltAsync.OnClient("vehicle-interaction:closeTrunkRequest", CloseVehicleTrunk);
            Alt.OnClient("trunk:putItemToEquipment", PutItemToEquipmentFromTrunk);
            Alt.OnClient("trunk:putItemToVehicleTrunk", PutItemToVehicleTrunk);
        }

        private async Task OnEnterPlayerVehicle(IVehicle vehicle, IPlayer player, byte seat) => await AltAsync.Do(async () =>
        {
            if (!player.HasData("current:trunk")) return;
            if ((int)seat != 1) return;

            Alt.Log("Dotarł event z OnEnterPlayerVehicle TRUNK");

            await DisposeVehicleTrunk(vehicle, player);
        });

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
            IVehicle vehicle = (IVehicle)args[0];
            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();

            if (vehicleEntity == null) return;

            IColShape trunkColShape = Alt.CreateColShapeCylinder(new Position(vehicle.Position.X, vehicle.Position.Y - 4, vehicle.Position.Z), 1f, 2f);
            trunkColShape.SetData("vehicle:trunk", vehicleEntity);


            MarkerModel marker = new MarkerModel()
            {
                Type = 27,
                Dimension = vehicle.Dimension,
                PosX = vehicle.Position.X,
                PosY = vehicle.Position.Y - 4,
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
            EntityHelper.Add(marker);
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

        public async Task CloseVehicleTrunk(IPlayer player, object[] args) => await AltAsync.Do(async () =>
        {
            player.GetData("current:vehicle-trunk", out IColShape trunkColshape);
            if (trunkColshape == null) return;

            trunkColshape.GetData("vehicle:trunk", out VehicleEntity vehicleEntity);
            if (vehicleEntity == null) return;

            Alt.Log("Dotarł event CloseVehicleTrunk");

            await DisposeVehicleTrunk(vehicleEntity.GameVehicle, player);
        });

        public void PutItemToEquipmentFromTrunk(IPlayer player, object[] args)
        {
            int itemID = (int)(long)args[0];

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
            ItemModel itemToChange = vehicleEntity.DbModel.ItemsInVehicle.SingleOrDefault(item => item.Id == itemID);
            vehicleEntity.DbModel.ItemsInVehicle.Remove(itemToChange);
            characterEntity.DbModel.Items.Add(itemToChange);
        }

        private void PutItemToVehicleTrunk(IPlayer player, object[] args)
        {
            int itemID = (int)(long)args[0];

            player.GetData("current:vehicle-trunk", out IColShape trunkColshape);
            if (trunkColshape == null) return;

            trunkColshape.GetData("vehicle:trunk", out VehicleEntity vehicleEntity);
            if (vehicleEntity == null) return;
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (vehicleEntity.DbModel.OwnerId != characterEntity.DbModel.Id)
            {
                //TODO: Dorobić że typ grupy police może przeszukiwać bagażniki
                player.SendErrorNotify("Nie masz uprawnień", "Nie jesteś właścicielem tego pojazdu");
                return;
            }
            ItemModel itemToChange = characterEntity.DbModel.Items.SingleOrDefault(item => item.Id == itemID);

            if(itemToChange.ItemInUse)
            {
                player.SendErrorNotify("Używasz ten przedmiot!", "Musisz odużyć przedmiot, aby móc włożyć go do samochodu");
                return;
            }

            characterEntity.DbModel.Items.Remove(itemToChange);
            vehicleEntity.DbModel.ItemsInVehicle.Add(itemToChange);
        }

        private async Task DisposeVehicleTrunk(IVehicle vehicle, IPlayer player) => await AltAsync.Do(async () =>
        {
            player.GetData("current:vehicle-trunk", out IColShape trunkColshape);
            if (trunkColshape == null && vehicle.GetVehicleEntity().TrunkOpen)
            {
                await MarkerHelper.RemoveGlobalMarker($"VEHICLE_TRUNK_MARKER{vehicle.GetVehicleEntity().DbModel.Id}");
                return;
            }

            trunkColshape.GetData("vehicle:trunk", out VehicleEntity vehicleEntity);
            if (vehicleEntity == null) return;

            await MarkerHelper.RemoveGlobalMarker($"VEHICLE_TRUNK_MARKER{vehicleEntity.DbModel.Id}");
            trunkColshape.Remove();

            await vehicleEntity.GameVehicle.SetDoorStateAsync(VehicleDoor.Trunk, VehicleDoorState.Closed);
        });
    }
}
