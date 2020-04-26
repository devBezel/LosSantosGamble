using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleTrunkScript : IScript
    {
        //public VehicleTrunkScript()
        //{
        //    //Alt.OnClient("vehicle-interaction:openTrunkRequest", OpenVehicleTrunkRequest);
        //    //AltAsync.OnColShape += OnEnterColshape;
        //    //AltAsync.OnPlayerEnterVehicle += OnEnterPlayerVehicle;
        //    //Alt.OnClient("vehicle-trunk:open", OpenVehicleTrunk);
        //    //AltAsync.OnClient("vehicle-interaction:closeTrunkRequest", CloseVehicleTrunk);
        //    //Alt.OnClient("trunk:putItemToEquipment", PutItemToEquipmentFromTrunk);
        //    //Alt.OnClient("trunk:putItemToVehicleTrunk", PutItemToVehicleTrunk);
        //}

        [AsyncScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public async Task OnEnterPlayerVehicle(IVehicle vehicle, IPlayer player, byte seat) => await AltAsync.Do(() =>
        {
            if (!vehicle.GetVehicleEntity().TrunkOpen) return;
            //if ((int)seat != 1) return;

            Alt.Log("Dotarł event z OnEnterPlayerVehicle TRUNK");

            DisposeVehicleTrunk(vehicle, player);
        });

        [AsyncScriptEvent(ScriptEventType.ColShape)]
        public async Task OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
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
        [ClientEvent("vehicle-interaction:openTrunkRequest")]
        public void OpenVehicleTrunkRequest(IPlayer player, IVehicle vehicle, string positionJson)
        {
            //IVehicle vehicle = (IVehicle)args[0];
            
            Vector3 positionTrunk = JsonConvert.DeserializeObject<Vector3>(positionJson);


            Alt.Log("Doszedl event trunk");
            Alt.Log($"xx: {positionTrunk.X} y: {positionTrunk.Y} z: {positionTrunk.Z}");

            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();

            if (vehicleEntity == null) return;

            IColShape trunkColShape = Alt.CreateColShapeCylinder(new Position(positionTrunk.X, positionTrunk.Y, positionTrunk.Z), 2f, 2f);
            trunkColShape.SetData("vehicle:trunk", vehicleEntity);
            vehicle.SetData("current:vehicle-trunks", trunkColShape);


            DrawTextModel drawTextModel = new DrawTextModel()
            {
                Text = "Kliknij ~g~E ~w~ aby otworzyć bagażnik",
                X = positionTrunk.X,
                Y = positionTrunk.Y,
                Z = positionTrunk.Z,
                Dimension = vehicle.Dimension,
                UniqueID = $"VEHICLE_TRUNK_DRAW_TEXT{vehicleEntity.DbModel.Id}"
            };

            DrawTextHelper.CreateGlobalDrawText(drawTextModel);

            vehicleEntity.TrunkOpen = true;
            EntityHelper.Add(drawTextModel);
        }
        [ClientEvent("vehicle-trunk:open")]
        public void OpenVehicleTrunk(IPlayer player)
        {
            player.GetData("current:vehicle-trunk", out IColShape trunkColshape);
            if (trunkColshape == null) return;

            trunkColshape.GetData("vehicle:trunk", out VehicleEntity vehicleEntity);
            if (vehicleEntity == null) return;

            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;


            if(vehicleEntity.IsGroupVehicle)
            {
                GroupEntity vehicleGroupOwner = vehicleEntity.GroupOwner;
                if (vehicleGroupOwner == null) 
                    return;

                if(characterEntity.OnDutyGroup == null)
                {
                    player.SendErrorNotify("Musisz być na służbie grupy, aby otworzyć bagażnik");
                    return;
                }

                if (vehicleGroupOwner != characterEntity.OnDutyGroup)
                {
                    player.SendChatMessageError("Ten pojazd nie należy do twojej grupy");
                    return;
                }

                GroupWorkerModel worker = characterEntity.OnDutyGroup.DbModel.Workers.First(c => c.CharacterId == characterEntity.DbModel.Id);
                if(!vehicleGroupOwner.CanPlayerVehicle(worker))
                {
                    player.SendChatMessageError("Nie masz uprawnień do tego, aby otworzyć bagażnik pojazdu");
                    return;
                }

            }
            else
            {
                if (vehicleEntity.DbModel.OwnerId != characterEntity.DbModel.Id)
                {
                    //TODO: Dorobić że typ grupy police może przeszukiwać bagażniki
                    player.SendErrorNotify("Nie masz uprawnień", "Nie jesteś właścicielem tego pojazdu");
                    return;
                }
            }


            player.Emit("vehicle-trunk:data",
                characterEntity.DbModel.Items,
                vehicleEntity.DbModel.ItemsInVehicle);
        }

        [ClientEvent("vehicle-interaction:closeTrunkRequest")]
        public void CloseVehicleTrunk(IPlayer player, IVehicle vehicle)
        {
            //IVehicle vehicle = (IVehicle)args[0];

            DisposeVehicleTrunk(vehicle, player);
        }
        [ClientEvent("trunk:putItemToEquipment")]
        public void PutItemToEquipmentFromTrunk(IPlayer player, int itemID)
        {
            //int itemID = (int)(long)args[0];

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
        [ClientEvent("trunk:putItemToVehicleTrunk")]
        public void PutItemToVehicleTrunk(IPlayer player, int itemID)
        {
            //int itemID = (int)(long)args[0];

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
        public void DisposeVehicleTrunk(IVehicle vehicle, IPlayer player)
        {
            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();
            if (vehicleEntity == null) return;


            vehicle.GetData("current:vehicle-trunks", out IColShape trunkColshape);
            if (trunkColshape == null && !vehicleEntity.TrunkOpen)
            {
                return;
            }

            vehicleEntity.TrunkOpen = false;

            DrawTextHelper.RemoveGlobalDrawText($"VEHICLE_TRUNK_DRAW_TEXT{vehicleEntity.DbModel.Id}");
            trunkColshape.Remove();

            vehicleEntity.GameVehicle.SetDoorState(VehicleDoor.Trunk, VehicleDoorState.Closed);
        }
    }
}
