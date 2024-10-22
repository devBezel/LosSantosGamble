﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Core.Streamers.ObjectStreamer;
using LSG.GM.Entities.Common.Atm;
using LSG.GM.Entities.Common.Bus;
using LSG.GM.Entities.Common.Shop;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Buidling;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Entities.Core.Warehouse;
using LSG.GM.Entities.Job;
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
        private static readonly List<DynamicObject> WorldDynamicObjects = new List<DynamicObject>();


        private static readonly List<AccountEntity> Accounts = new List<AccountEntity>();

        private static readonly List<AtmEntity> Atms = new List<AtmEntity>();
        private static readonly List<BusEntity> BusStops = new List<BusEntity>();
        private static readonly List<BuildingEntity> Buildings = new List<BuildingEntity>();
        private static readonly List<ShopEntity> Shops = new List<ShopEntity>();
        private static readonly List<GroupEntity> Groups = new List<GroupEntity>();
        private static readonly List<WarehouseEntity> Warehouses = new List<WarehouseEntity>();
        private static readonly List<WarehouseOrderEntity> WarehouseOrders = new List<WarehouseOrderEntity>();

        private static readonly List<ItemInWorldModel> ItemInWorld = new List<ItemInWorldModel>();

        private static readonly List<JobCenterEntity> JobCenters = new List<JobCenterEntity>();
        private static readonly List<JobEntity> Jobs = new List<JobEntity>();
        private static readonly List<Smartphone> CurrentSmartphones = new List<Smartphone>();

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

        public static bool AccountLogged(int id)
        {
            AccountEntity account = Accounts.Find(x => x.DbModel.Id == id);
            if (account == null)
            {
                return false;
            }
            else
            {
                return account.IsLogged;
            }
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
        public static void RemoveVehicleTrunkDrawText(string uniqueID)
        {
            DrawTextModel trunkDrawTextToRemove = VehicleTrunkDrawsText.Find(x => x.UniqueID == uniqueID);
            VehicleTrunkDrawsText.Remove(trunkDrawTextToRemove);
        }

        public static void Add(DynamicObject dynamicObject) => WorldDynamicObjects.Add(dynamicObject);
        public static void Remove(DynamicObject dynamicObject) => WorldDynamicObjects.Remove(dynamicObject);
        public static DynamicObject GetById(uint Id)
        {
            DynamicObject dynamicObject = WorldDynamicObjects.FirstOrDefault(x => x.Id == Id);

            return dynamicObject;
        }

        public static void Add(WarehouseEntity warehouseModel) => Warehouses.Add(warehouseModel);
        public static WarehouseEntity GetWarehouseByGroupId(int id) => Warehouses.SingleOrDefault(x => x.DbModel.GroupId == id);

        public static void Add(JobEntity jobEntity) => Jobs.Add(jobEntity);

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
            VehicleDb vehicle = Singleton.GetDatabaseInstance().Vehicles.Include(item => item.ItemsInVehicle).Include(upgrade => upgrade.VehicleUpgrades).SingleOrDefault(v => v.Id == id);

            return vehicle;
        }

        public static List<VehicleDb> GetCharacterVehicleDatabaseList(int id)
        {
            List<VehicleDb> vehicles = Singleton.GetDatabaseInstance().Vehicles.Where(v => v.Owner.Id == id).ToList();

            return vehicles;
        }

        public static void Add(BuildingEntity buildingEntity) => Buildings.Add(buildingEntity);

        public static void Remove(BuildingEntity buildingEntity) => Buildings.Remove(buildingEntity);

        public static void Add(WarehouseOrderEntity warehouseOrderEntity) => WarehouseOrders.Add(warehouseOrderEntity);
        public static void Remove(WarehouseOrderEntity warehouseOrderEntity) => WarehouseOrders.Remove(warehouseOrderEntity);
        public static WarehouseOrderEntity GetWarehouseOrderById(int id) => WarehouseOrders.SingleOrDefault(o => o.DbModel.Id == id);

        public static void Add(JobCenterEntity jobCenterEntity) => JobCenters.Add(jobCenterEntity);

        public static void Add(Smartphone smartphone) => CurrentSmartphones.Add(smartphone);
        public static void Remove(Smartphone smartphone) => CurrentSmartphones.Remove(smartphone);

        public static bool IsSmartphoneActive(int number)
        {
            Smartphone smartphone = CurrentSmartphones.FirstOrDefault(x => x.SmartphoneNumber == number);
            return smartphone == null ? false : true;
        }

        //TODO: Do naprawy
        public static CharacterEntity GetCharacterBySmartphoneNumber(int number)
        {
            if(IsSmartphoneActive(number))
            {
                CharacterEntity characterEntity = Accounts.FirstOrDefault(x => x.characterEntity.CurrentSmartphone != null && x.characterEntity.CurrentSmartphone.SmartphoneNumber == number).characterEntity;

                return characterEntity == null ? null : characterEntity;
            }

            return null;
        }


        public static List<WarehouseOrderEntity> GetAllWarehouseOrders()
        {
            return WarehouseOrders;
        }

        public static List<JobEntityModel> GetJobs()
        {
            List<JobEntityModel> jobEntityModels = new List<JobEntityModel>();
            Alt.Log($"Jobs length {Jobs.Count}");
            foreach (JobEntity job in Jobs)
            {
                Alt.Log($"Dodaje {job.JobEntityModel.JobName} do listy");
                jobEntityModels.Add(job.JobEntityModel);
            }

            return jobEntityModels;
        }

        // Tworzenie blipów, markerów itp (wszystko co jest lokalnie dla gracza gdy wchodzi na serwer)
        public static async Task LoadClientEntity(IPlayer player)
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

            //Do testów później tego nie będzie
            foreach (WarehouseEntity warehouse in Warehouses)
            {
                await player.CreateMarker(warehouse.Marker);
            }

            foreach (JobEntity job in Jobs)
            {
                await player.CreateMarker(job.Marker);
                await player.CreateBlip(job.Blip);
            }

            foreach (JobCenterEntity jobCenter in JobCenters)
            {
                await player.CreateMarker(jobCenter.Marker);
            }
        }

        public static async Task LoadServerEntity()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();

            using (UnitOfWork unit = new UnitOfWork(ctx))
            {
                await BuildingEntity.LoadBuildingsAsync(unit);
                await AtmEntity.LoadAtmsAsync(unit);
                await BusEntity.LoadBusAsync(unit);
                await ShopEntity.LoadShopAsync(unit);
                await GroupEntity.LoadGroupsAsync(unit);
                await WarehouseOrderEntity.LoadWarehouseOrdersAsync();
            }

            JobEntity courierJob = new JobEntity(new JobEntityModel()
            {
                JobName = "Kurier",
                VehicleModel = AltV.Net.Enums.VehicleModel.Boxville2,
                RespawnVehicle = true,
                Position = new Position(26.1626f, -1300.59f, 29.2124f),
                RespawnVehiclePosition = new Position(36.9495f, -1283.84f, 29.2799f),
                RespawnVehicleRotation = new Rotation(0, 0, 1.53369f),
                JobType = JobType.Courier,
                MaxSalary = 400
            });
            courierJob.Create();

            JobEntity junkerJob = new JobEntity(new JobEntityModel()
            {
                JobName = "Śmieciarz",
                VehicleModel = AltV.Net.Enums.VehicleModel.Trash,
                RespawnVehicle = true,
                Position = new Position(500.334f, -652.009f, 24.8989f),
                RespawnVehiclePosition = new Position(508.286f, -609.771f, 25.1348f),
                RespawnVehicleRotation = new Rotation(0, 0, 1.63264f),
                JobType = JobType.Junker,
                MaxSalary = 400
            });
            junkerJob.Create();

            JobCenterEntity jobCenter = new JobCenterEntity(new JobCenterModel()
            {
                Id = 0,
                Position = new Position(104.73f, -934.075f, 29.8022f),
                Jobs = EntityHelper.GetJobs()
            });

            jobCenter.Spawn();
        }
    }
}
