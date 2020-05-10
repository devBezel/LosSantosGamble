using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Enums;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Economy.Groups.Base
{
    public class Mechanic : GroupEntity
    {
        /* Prawa
         * 1. Naprawianie pojazdów
         * 2. Tuningowanie pojazdów
         * 3. Holowanie pojazdów
         * 
         * 
         **/

        public Mechanic(GroupModel group) : base(group)
        {

        }

        public bool CanPlayerRepairVehicle(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity))
                return false;

            GroupWorkerModel worker = DbModel.Workers.FirstOrDefault(x => x.CharacterId == accountEntity.characterEntity.DbModel.Id);

            return worker.GroupRank.Rights.HasFlag(GroupRights.First);
        }

        public bool CanPlayerTuningVehicle(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity))
                return false;

            GroupWorkerModel worker = DbModel.Workers.FirstOrDefault(x => x.CharacterId == accountEntity.characterEntity.DbModel.Id);

            return worker.GroupRank.Rights.HasFlag(GroupRights.Second);
        }


    }
}
