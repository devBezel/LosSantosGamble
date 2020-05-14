using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Job;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Jobs.Scripts
{
    public class CourierJobScript : IScript
    {

        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnPlayerEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            //if (!state) return;

            //if (colShape.HasData("job:data"))
            //{
            //    if (entity is IPlayer player)
            //    {
            //        colShape.GetData("job:data", out JobEntity jobEntity);

            //        if (jobEntity != null)
            //        {
            //            if (jobEntity.Job is CourierJob courierJob)
            //            {
            //                CharacterEntity worker = player.GetAccountEntity().characterEntity;

            //                if (worker == null) return;

            //                if (worker.DbModel.JobType == courierJob.JobEntityModel.JobType)
            //                {
            //                    if (worker.CasualJob == null)
            //                    {
            //                        courierJob.Start(worker);
            //                    }
            //                    else
            //                    {
            //                        courierJob.Stop(worker);
            //                    }
            //                }
            //                else
            //                {
            //                    player.SendChatMessageInfo("Nie jesteś zatrudniony w tej pracy, aby móc tu pracować zatrudnij się w urzędzie pracy");
            //                }
            //            }
            //        }
            //    }

            //}
        }
    }
}
