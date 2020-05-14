using AltV.Net;
using AltV.Net.ColShape;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Job;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using IColShape = AltV.Net.Elements.Entities.IColShape;

namespace LSG.GM.Economy.Jobs
{
    public class JobScript : IScript
    {
        public JobScript()
        {
            JobEntity courierJob = new JobEntity(new JobEntityModel()
            {
                JobName = "Kurier",
                VehicleModel = AltV.Net.Enums.VehicleModel.Boxville2,
                Position = new Position(26.1626f, -1300.59f, 29.2124f),
                RespawnVehiclePosition = new Position(36.9495f, -1283.84f, 29.2799f),
                RespawnVehicleRotation = new Rotation(0, 0, 1.53369f),
                JobType = DAL.Enums.JobType.Courier
            });
            courierJob.Create();
        }

        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnPlayerEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            if (!state) return;

            if (colShape.HasData("job:data"))
            {
                if (entity is IPlayer player)
                {
                    colShape.GetData("job:data", out JobEntity jobEntity);

                    if (jobEntity != null)
                    {
                        if (jobEntity.Job is CourierJob courierJob)
                        {
                            CharacterEntity worker = player.GetAccountEntity().characterEntity;

                            if (worker == null) return;

                            if (worker.DbModel.JobType == courierJob.JobEntityModel.JobType)
                            {
                                if (worker.CasualJob == null)
                                {
                                    courierJob.Start(worker);
                                }
                                else
                                {
                                    courierJob.Stop(worker);
                                }
                            }
                            else
                            {
                                player.SendChatMessageInfo("Nie jesteś zatrudniony w tej pracy, aby móc tu pracować zatrudnij się w urzędzie pracy");
                            }
                        }
                    }
                }
            }
        }
    }
}
