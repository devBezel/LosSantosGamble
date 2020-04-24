using LSG.DAL.Database.Models.GroupModels;
using LSG.GM.Economy.Groups.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Group
{
    public class GroupEntityFactory
    {
        public GroupEntity Create(GroupModel groupModel)
        {
            switch (groupModel.GroupType)
            {
                case DAL.Enums.GroupType.Police: return new Police(groupModel);
                case DAL.Enums.GroupType.Paramedic: return new Paramedic(groupModel);
                default:
                    throw new NotSupportedException($"Ta grupa ${groupModel.GroupType} nie jest wspierana");
            }
        }
    }
}
