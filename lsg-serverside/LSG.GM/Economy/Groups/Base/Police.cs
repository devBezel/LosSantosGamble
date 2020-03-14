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
    public class Police : GroupEntity
    {
        /* PRAWA POLICJI
         * 1 Kajdanki
         * 
         */
        public Police(GroupModel groupModel) : base(groupModel)
        {

        }

        public bool CanPlayerDoPolice(AccountEntity accountEntity)
        {
            GroupWorkerModel groupWorker = DbModel.Workers.First(w => w.CharacterId == accountEntity.characterEntity.DbModel.Id);

            return groupWorker.Rights.HasFlag(GroupRights.First);
        }
    }
}
