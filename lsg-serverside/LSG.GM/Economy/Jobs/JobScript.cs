using AltV.Net;
using AltV.Net.ColShape;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Enums;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Job;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using IColShape = AltV.Net.Elements.Entities.IColShape;

namespace LSG.GM.Economy.Jobs
{
    public class JobScript : IScript
    {

        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnPlayerEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            if (entity is IPlayer player)
            {
                if (state)
                {
                    if (colShape.HasData("job:data"))
                    {
                        colShape.GetData("job:data", out JobEntity jobEntity);

                        if (jobEntity != null)
                        {
                            CharacterEntity worker = player.GetAccountEntity().characterEntity;

                            if (worker == null) return;

                            if (worker.DbModel.JobType == jobEntity.JobEntityModel.JobType)
                            {
                                new Interaction(worker.AccountEntity.Player, "job:showWorkWindow", "aby otworzyć menu pracy");
                                player.SetData("current:startJobColshape", jobEntity);
                            }
                            else
                            {
                                player.SendChatMessageInfo("Nie jesteś zatrudniony w tej pracy, aby móc tu pracować zatrudnij się w urzędzie pracy");
                            }
                        }
                    }
                }
                else
                {
                    if (player.HasData("current:startJobColshape"))
                    {
                        player.DeleteData("current:startJobColshape");
                    }
                }
            }



        }

        [ClientEvent("job:showWorkWindow")]
        public void StartOrStopCasualJob(IPlayer player)
        {
            CharacterEntity worker = player.GetAccountEntity().characterEntity;
            player.GetData("current:startJobColshape", out JobEntity currentCasualWork);

            if (currentCasualWork == null)
                return;

            bool playerWorking = worker.CasualJob != null ? true : false;
            player.Emit("job:showWorkWindow", playerWorking, currentCasualWork.JobEntityModel);   
        }


        [ClientEvent("job:start")]
        public void StartJob(IPlayer player)
        {
            CharacterEntity worker = player.GetAccountEntity().characterEntity;
            if(player.HasData("current:startJobColshape"))
            {
                player.GetData("current:startJobColshape", out JobEntity currentCasualWork);
                if (worker.CasualJob == null)
                {
                    currentCasualWork.Start(worker);
                }
                else
                {
                    currentCasualWork.Stop(worker);
                }
            }
            else
            {
                player.SendChatMessageError("Musisz być w kółku, aby rozpocząć pracę");
            }
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerLeaveVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null) return;

            int timeToDeleteVehicle = 180;
            int spentTime = 0;

            if(characterEntity.CasualJob != null)
            {
                if(characterEntity.CasualJobVehicle != null)
                {
                    if (characterEntity.CasualJobVehicle.VehicleEntity.GameVehicle == vehicle)
                    {
                        player.SendChatMessageInfo("Opuściłeś pojazd służbowy, masz 3 minuty, aby do niego wrócić - jeżeli tego nie zrobisz, twoja praca zostanie zakończona");

                        Timer playerExitJobVehicle = new Timer(10000);
                        characterEntity.CasualJobVehicle.OutOfTheVehicleTimer = playerExitJobVehicle;
                        playerExitJobVehicle.Start();
                        playerExitJobVehicle.Elapsed += (o, args) =>
                        {
                            spentTime += 10;

                            if (spentTime >= timeToDeleteVehicle)
                            {
                                characterEntity.CasualJob.Stop(characterEntity);
                                playerExitJobVehicle.Stop();
                                playerExitJobVehicle.Dispose();
                                player.SendChatMessageInfo("Twoja praca została zakończona ponieważ nie wróciłeś na czas do pojazdu");
                            }
                        };
                    }
                }
            }
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null) return;

            if (characterEntity.CasualJob != null)
            {
                if (characterEntity.CasualJobVehicle != null)
                {
                    if (characterEntity.CasualJobVehicle.VehicleEntity.GameVehicle == vehicle)
                    {
                        if(characterEntity.CasualJobVehicle.OutOfTheVehicleTimer != null)
                        {
                            characterEntity.CasualJobVehicle.OutOfTheVehicleTimer.Stop();
                            characterEntity.CasualJobVehicle.OutOfTheVehicleTimer.Dispose();
                        }
                    }
                }
            }
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(IPlayer player, string reason)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null)
                return;

            if(characterEntity.CasualJob != null)
            {
                characterEntity.CasualJob.Stop(characterEntity);
            }
        }
    }
}
