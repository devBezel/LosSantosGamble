using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Dto.Vehicle;
using LSG.DAL.Database.Models.GroupModels;
//using LSG.GM.Entities.Admin;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleScript : IScript
    {
        //public VehicleScript()
        //{
        //    Alt.OnClient("vehicle:spawnVehicle", SpawnOwnVehicle);
        //    Task.Run(() =>
        //    {
        //        AltAsync.OnPlayerLeaveVehicle += OnPlayerLeaveVehicle;
        //    });

        //    //AltAsync.OnPlayerEnterVehicle += OnPlayerEnterVehicle;
        //    AltAsync.OnPlayerLeaveVehicle += OnPlayerLeaveVehicle;
        //    Alt.OnPlayerChangeVehicleSeat += OnPlayerChangeVehicleSeat;
        //}

        [ScriptEvent(ScriptEventType.PlayerChangeVehicleSeat)]
        public void OnPlayerChangeVehicleSeat(IVehicle vehicle, IPlayer player, byte oldSeat, byte newSeat)
        {
            player.Emit("player:changeVehicleSeat", (int)oldSeat, (int)newSeat);
        }

        [AsyncScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public async Task OnPlayerEnterVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            await player.EmitAsync("player:enterVehicle", (int)seat);
        }

        [AsyncScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public async Task OnPlayerLeaveVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            VehicleEntity vehicleEntity = vehicle.GetVehicleEntity();
            if (seat == 1)
            {
                if (vehicleEntity == null) return;

                vehicleEntity.Save();
            }

           await player.EmitAsync("player:leaveVehicle", seat);
        }

        [Command("v")]
        public async Task OpenVehicleCEFWindowCMD(IPlayer player)
        {
            await player.EmitAsync("vehicle:openWindow", EntityHelper.GetCharacterVehicleDatabaseList(player.GetAccountEntity().characterEntity.DbModel.Id));
        }

        [ClientEvent("vehicle-interaction:getVehicleInfo")]
        public void GetVehicleInfo(IPlayer player)
        {
            if (player.Vehicle == null)
                return;

            VehicleEntity vehicleEntity = player.Vehicle.GetVehicleEntity();
            if (vehicleEntity == null)
                return;

            player.Emit("vehicle-script:vehicleInfo", vehicleEntity.DbModel.VehicleUpgrades);
        }

        [ClientEvent("vehicle:spawnVehicle")]
        public void SpawnOwnVehicle(IPlayer player, int vehicleId)
        {
   
            VehicleEntity spawnedVehicle = EntityHelper.GetSpawnedVehicleById(vehicleId);
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null)
                return;


            if (spawnedVehicle != null)
            {
                if(player.Vehicle == spawnedVehicle.GameVehicle)
                {
                    player.SendErrorNotify(null, $"Aby odpspawnić pojazd musisz z niego wyjść");
                    return;
                }

                if (spawnedVehicle.TrunkOpen)
                {
                    player.SendErrorNotify(null, "Aby odspawnić pojazd musisz zamknąć bagażnik");
                    return;
                }

                if (spawnedVehicle.IsGroupVehicle)
                {
                    if (characterEntity.OnDutyGroup == null)
                    {
                        player.SendChatMessageError("Musisz być na służbie, aby zrespić pojazd");
                        return;
                    }

                    if(characterEntity.OnDutyGroup != spawnedVehicle.GroupOwner)
                    {
                        player.SendChatMessageError("Ten pojazd nie należy do tej grupy");
                        return;
                    }

                    GroupWorkerModel worker = characterEntity.OnDutyGroup.DbModel.Workers.First(c => c.CharacterId == characterEntity.DbModel.Id);
                    if (!characterEntity.OnDutyGroup.CanPlayerVehicle(worker))
                    {
                        player.SendChatMessageError("Nie masz uprawnień do chowania pojazdów w tej grupie");
                        return;
                    }

                    spawnedVehicle.GroupOwner = null;
                }
                else
                {
                    player.GetAccountEntity().characterEntity.RespawnVehicleCount--;
                }

                spawnedVehicle.Dispose();

                player.SendSuccessNotify(null, $"Twój pojazd o ID {spawnedVehicle.DbModel.Id} został odspawniony");

                return;
            }

            VehicleEntity vehicle = new VehicleEntity(EntityHelper.GetVehicleDatabaseById(vehicleId));
            //vehicle.Spawn();


            if(vehicle.IsGroupVehicle)
            {
                if (characterEntity.OnDutyGroup == null)
                {
                    player.SendChatMessageError("Musisz być na służbie, aby zrespić pojazd");
                    return;
                }

                if (characterEntity.OnDutyGroup.DbModel.Id != vehicle.DbModel.GroupId)
                {

                    player.SendChatMessageError("Ten pojazd nie należy do tej grupy");
                    return;
                }

                GroupWorkerModel worker = characterEntity.OnDutyGroup.DbModel.Workers.FirstOrDefault(c => c.CharacterId == characterEntity.DbModel.Id);
                if (!characterEntity.OnDutyGroup.CanPlayerVehicle(worker))
                {
                    player.SendChatMessageError("Nie masz uprawnień do respienia pojazdów w tej grupie");
                    return;
                }

                vehicle.Spawn();
                vehicle.GroupOwner = characterEntity.OnDutyGroup;
            }
            else
            {
                if (characterEntity.RespawnVehicleCount > 3 && !player.GetAccountEntity().HasPremium)
                {
                    if (player.GetAccountEntity().OnAdminDuty) return;

                    player.SendErrorNotify(null, $"Aby zrespić więcej niż 3 pojazdy musisz posiadać premium");
                    //vehicle.Dispose();
                    return;
                }

                vehicle.Spawn();
                characterEntity.RespawnVehicleCount++;
            }


            player.SendSuccessNotify(null, $"Twój pojazd o ID {vehicle.DbModel.Id} został zespawniony");

        }
    }
}
