using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core.Buidling;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class BuildingExtenstion
    {
        public static BuildingEntity GetBuildingEntity(this IColShape colshape)
        {
            colshape.GetData("building:data", out BuildingEntity result);
            return result;
        }
    }
}
