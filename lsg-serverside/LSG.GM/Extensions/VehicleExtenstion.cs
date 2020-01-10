using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class VehicleExtenstion
    {
        public static VehicleEntity GetVehicleEntity(this IVehicle vehicle)
        {
            vehicle.GetData("account:data", out VehicleEntity vehicleEntity);

            return vehicleEntity;
        }
    }
}
