using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Entities
{
    public static class EntityHelper
    {

        public static VehicleEntity GetSpawnedVehicleById(int id)
        {
            IVehicle veh =  Alt.GetAllVehicles().SingleOrDefault(v => v.GetData("vehicle:id", out int vehicleId) && vehicleId == id);
            if(veh != null)
            {
                if (veh.GetData("vehicle:data", out VehicleEntity vehicleData))
                {
                    return vehicleData;
                }
            }
            return null;
        }
         
        public static VehicleDb GetVehicleDatabaseById(int id)
        {
            VehicleDb vehicle = Singleton.GetDatabaseInstance().Vehicles.SingleOrDefault(v => v.Id == id);

            return vehicle; 
        }
        // Tworzenie blipów, grup itp
    }
}
