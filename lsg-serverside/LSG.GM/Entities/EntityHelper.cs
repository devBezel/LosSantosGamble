using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Common.Atm;
using LSG.GM.Entities.Common.Bus;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Entities
{
    public static class EntityHelper
    {

        private static readonly List<AccountEntity> Accounts = new List<AccountEntity>();

        private static readonly List<AtmEntity> Atms = new List<AtmEntity>();
        private static readonly List<BusEntity> BusStops = new List<BusEntity>();

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

        public static void Add(AtmEntity atmEntity)
        {
            if (Atms.Any(a => a.DbModel.Id == atmEntity.DbModel.Id)) return;
            Atms.Add(atmEntity);
        }

        public static void Add(BusEntity busEntity) => BusStops.Add(busEntity);

        public static BusEntity GetById(int id)
        {
            BusEntity bus = BusStops.SingleOrDefault(x => x.DbModel.Id == id);

            return bus;
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
        // Tworzenie blipów, markerów itp (wszystko co jest lokalnie dla gracza gdy wchodzi na serwer)
        public static async Task LoadClientEntity(IPlayer player) => await AltAsync.Do(async () =>
        {
            foreach (AtmEntity atm in Atms)
            {
                await player.CreateBlip(atm.BlipModel);
                await player.CreateMarker(atm.MarkerModel);
            }

            foreach (BusEntity bus in BusStops)
            {
                await player.CreateBlip(bus.BlipModel);
                await player.CreateMarker(bus.MarkerModel);
            }
        });

        public static async Task LoadServerEntity() => await AltAsync.Do(async () =>
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();

            using(UnitOfWork unit = new UnitOfWork(ctx))
            {
                foreach (Atm atm in await unit.AtmRepository.GetAll())
                {
                    AtmEntity atmEntity = new AtmEntity(atm);
                    await atmEntity.Spawn();
                }

                foreach (BusStop busStop in await unit.BusRepository.GetAll())
                {
                    BusEntity busEntity = new BusEntity(busStop);
                    await busEntity.Spawn();
                }
            }
        });

    }
}
