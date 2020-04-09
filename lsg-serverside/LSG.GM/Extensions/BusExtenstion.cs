using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Common.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class BusExtenstion
    {
        public static BusEntity GetBusEntity(this IColShape colshape)
        {
            colshape.GetData("bus:data", out BusEntity bus);

            return bus;
        }
    }
}
