using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Enums;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Job;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Jobs
{
    public class JobCenterScript : IScript
    {


        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnPlayerEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            if(entity is IPlayer player)
            {
                if (colShape.HasData("job-center:data"))
                {
                    if (state)
                    {
                        colShape.GetData("job-center:data", out JobCenterEntity jobCenterEntity);
                        player.SetData("current:job-center", jobCenterEntity);

                        new Interaction(player, "job-center:showWindow", "aby otworzyć menu urzędu pracy");
                    }
                    else
                    {
                        player.DeleteData("current:job-center");
                    }
                }
            }
        }

        [ClientEvent("job-center:showWindow")]
        public void JobCenterShowWindow(IPlayer player)
        {
            if(player.HasData("current:job-center"))
            {
                player.GetData("current:job-center", out JobCenterEntity jobCenterEntity);

                player.Emit("job-center:showWindow", (int)player.GetAccountEntity().characterEntity.DbModel.JobType, jobCenterEntity.JobCenterModel.Jobs);
            } 
            else
            {
                player.SendChatMessageError("Musisz być w kółku, aby otworzyć menu pracy");
            }
        }

        [ClientEvent("job-center:setJob")]
        public void SetJob(IPlayer player, int jobType)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null) return;

            characterEntity.DbModel.JobType = (JobType)jobType;
            player.SendChatMessageInfo("Twoja praca została zmieniona z powodzeniem!");
        }
    }
}
