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
    public class Paramedic : GroupEntity
    {
        /* Prawa medyków
         * 1. Reanimowanie,
         * 2. Zakuwanie
         * 3. Prowadzenie,
         * 4. MDT
         * */

        public Paramedic(GroupModel group) : base(group)
        {

        }

        public bool CanPlayerResuscitation(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity)) 
                return false;

            GroupWorkerModel groupWorker = DbModel.Workers.First(w => w.CharacterId == accountEntity.characterEntity.DbModel.Id);
            return groupWorker.GroupRank.Rights.HasFlag(GroupRights.First);
        }

        public bool CanPlayerCuff(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity))
                return false;

            GroupWorkerModel groupWorker = DbModel.Workers.First(w => w.CharacterId == accountEntity.characterEntity.DbModel.Id);
            return groupWorker.GroupRank.Rights.HasFlag(GroupRights.Second);
        }

        public bool CanPlayerMoveOtherPlayer(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity)) 
                return false;

            GroupWorkerModel groupWorker = DbModel.Workers.First(w => w.CharacterId == accountEntity.characterEntity.DbModel.Id);
            return groupWorker.GroupRank.Rights.HasFlag(GroupRights.Third);
        }
    }
}
