using AltV.Net;
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
         * 1 Kajdanki, przeszukiwanie i tak dalej
         * 2 Zakładanie blokad na koła
         * 3 MDT
         * 4 Wkładanie do więzienia
         */
        public Police(GroupModel groupModel) : base(groupModel)
        {

        }

        public bool CanPlayerDoPolice(AccountEntity accountEntity)
        {
            if (!ContainsWorker(accountEntity)) return false;
            
            GroupWorkerModel groupWorker = DbModel.Workers.First(w => w.CharacterId == accountEntity.characterEntity.DbModel.Id);
            return groupWorker.GroupRank.Rights.HasFlag(GroupRights.First);
        }
    }
}
