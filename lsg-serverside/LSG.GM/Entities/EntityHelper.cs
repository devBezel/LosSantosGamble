using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Common.Atm;
using LSG.GM.Entities.Common.Bus;
using LSG.GM.Entities.Common.Shop;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Buidling;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using Microsoft.EntityFrameworkCore;
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
        private static readonly List<BuildingEntity> Buildings = new List<BuildingEntity>();
        private static readonly List<ShopEntity> Shops = new List<ShopEntity>();
        private static readonly List<GroupEntity> Groups = new List<GroupEntity>();

        private static readonly List<ItemInWorldModel> ItemInWorld = new List<ItemInWorldModel>();

        private static readonly List<DrawTextModel> VehicleTrunkDrawsText = new List<DrawTextModel>();


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

        public static void Add(ShopEntity shopEntity) => Shops.Add(shopEntity);

        public static void Add(GroupEntity groupEntity) => Groups.Add(groupEntity);

        public static void Add(DrawTextModel drawTextModel) => VehicleTrunkDrawsText.Add(drawTextModel);
        public static void Remove(DrawTextModel drawTextModel) => VehicleTrunkDrawsText.Remove(drawTextModel);

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

        public static IEnumerable<GroupEntity> GetPlayerGroups(AccountEntity accountEntity)
        {
            return Groups.Where(g => g.DbModel.Workers.Any(x => x.Character?.Id == accountEntity.characterEntity.DbModel.Id));
        }

        public static void Add(ItemInWorldModel itemInWorldModel) => ItemInWorld.Add(itemInWorldModel);
        public static void Remove(ItemInWorldModel itemInWorldModel) => ItemInWorld.Remove(itemInWorldModel);

        public static VehicleDb GetVehicleDatabaseById(int id)
        {
            VehicleDb vehicle = Singleton.GetDatabaseInstance().Vehicles.Include(item => item.ItemsInVehicle).SingleOrDefault(v => v.Id == id);

            return vehicle;
        }

        public static List<VehicleDb> GetCharacterVehicleDatabaseList(int id)
        {
            List<VehicleDb> vehicles = Singleton.GetDatabaseInstance().Vehicles.Where(v => v.Owner.Id == id).ToList();

            return vehicles;
        }

        public static void Add(BuildingEntity buildingEntity) => Buildings.Add(buildingEntity);

        public static void Remove(BuildingEntity buildingEntity) => Buildings.Remove(buildingEntity);

        // Tworzenie blipów, markerów itp (wszystko co jest lokalnie dla gracza gdy wchodzi na serwer)
        public static async Task LoadClientEntity(IPlayer player) => await AltAsync.Do(async () =>
        {
            Alt.Log("[LoadClientEntity]");
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

            foreach (BuildingEntity building in Buildings)
            {
                Alt.Log($"building.BlipVisable: ${building.BlipVisable}");
                if (building.BlipVisable)
                {
                    Alt.Log("Tworze lokalnego blipa");
                    await player.CreateBlip(building.Blip);
                }

                await player.CreateMarker(building.InteriorMarker);
                await player.CreateMarker(building.ExteriorMarker);
            }

            foreach (ShopEntity shop in Shops)
            {
                await player.CreateBlip(shop.BlipModel);
                await player.CreateMarker(shop.MarkerModel);
            }

            foreach (DrawTextModel drawText in VehicleTrunkDrawsText)
            {
                player.CreateDrawText(drawText);
            }
        });

        public static async Task LoadServerEntity() => await AltAsync.Do(async () =>
        {
            Alt.Log("[LoadServerEntity]");
            RoleplayContext ctx = Singleton.GetDatabaseInstance();

            using (UnitOfWork unit = new UnitOfWork(ctx))
            {
                await BuildingEntity.LoadBuildingsAsync(unit);
                await AtmEntity.LoadAtmsAsync(unit);
                await BusEntity.LoadBusAsync(unit);
                await ShopEntity.LoadShopAsync(unit);
                await GroupEntity.LoadGroupsAsync(unit);
            }
        });

    }
}
