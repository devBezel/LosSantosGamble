using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
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

        private static readonly List<AccountEntity> Accounts = new List<AccountEntity>();

        public static void Add(AccountEntity account)
        {
            if (Accounts.Any(acc => acc.DbModel.Id == account.DbModel.Id))
            {
                //var accountActually = Accounts.FirstOrDefault(a => a.DbModel.Id == account.DbModel.Id);
                //accountActually = account;

                Alt.Log("Nastąpiło podowojenie użytkowników");
                return;
            }

            if (Accounts.Any(acc => acc == null))
                Accounts[Accounts.IndexOf(Accounts.First(acc => acc == null))] = account;
            else
                Accounts.Add(account);
        }

        public static void Remove(AccountEntity accountEntity)
        {
            Accounts.Remove(accountEntity);

        }


        public static VehicleEntity GetSpawnedVehicleById(int id)
        {
            IVehicle veh = Alt.GetAllVehicles().SingleOrDefault(v => v.GetData("vehicle:id", out int vehicleId) && vehicleId == id);
            if (veh != null)
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

        public static List<VehicleDb> GetCharacterVehicleDatabaseList(int id)
        {
            List<VehicleDb> vehicles = Singleton.GetDatabaseInstance().Vehicles.Where(v => v.Owner.Id == id).ToList();

            return vehicles;
        }
        // Tworzenie blipów, grup itp
    }
}
