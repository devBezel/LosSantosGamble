using AltV.Net;
using AltV.Net.Async;
using AltV.Net.ColoredConsole;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Buidling
{
    public class BuildingEntity
    {
        public BuildingModel DbModel { get; set; }
        public IColShape InteriorColshape { get; set; }
        public IColShape ExteriorColshape { get; set; }
        public MarkerModel InteriorMarker { get; set; }
        public MarkerModel ExteriorMarker { get; set; }

        public bool BlipVisable { get; set; } = true;
        public BlipModel Blip { get; set; }

        public List<IPlayer> PlayersInBuilding = new List<IPlayer>();

        public bool IsLocked { get; set; } = false;

        public BuildingEntity(BuildingModel model)
        {
            DbModel = model;
        }

        public async Task Spawn() => await AltAsync.Do(async () =>
        {
            InteriorColshape = Alt.CreateColShapeCylinder(new Position(DbModel.InternalPickupPositionX, DbModel.InternalPickupPositionY, DbModel.InternalPickupPositionZ - 0.9f), 1f, 2f);
            ExteriorColshape = Alt.CreateColShapeCylinder(new Position(DbModel.ExternalPickupPositionX, DbModel.ExternalPickupPositionY, DbModel.ExternalPickupPositionZ - 0.9f), 1f, 2f);
            ExteriorColshape.Dimension = DbModel.Id;

            InteriorMarker = new MarkerModel()
            {
                Type = 1,
                Dimension = 0,
                PosX = DbModel.InternalPickupPositionX,
                PosY = DbModel.InternalPickupPositionY,
                PosZ = DbModel.InternalPickupPositionZ - 0.9f,
                DirX = 0,
                DirY = 0,
                DirZ = 0,
                RotX = 0,
                RotY = 0,
                RotZ = 0,
                ScaleX = 1f,
                ScaleY = 1f,
                ScaleZ = 1f,
                Red = 0,
                Green = 153,
                Blue = 0,
                Alpha = 100,
                BobUpAndDown = false,
                FaceCamera = false,
                P19 = 2,
                Rotate = false,
                TextureDict = null,
                TextureName = null,
                DrawOnEnts = false,
                UniqueID = $"BUILDING_MARKER{DbModel.Id}"
            };

            ExteriorMarker = new MarkerModel()
            {
                Type = 1,
                Dimension = DbModel.Id,
                PosX = DbModel.ExternalPickupPositionX,
                PosY = DbModel.ExternalPickupPositionY,
                PosZ = DbModel.ExternalPickupPositionZ - 0.9f,
                DirX = 0,
                DirY = 0,
                DirZ = 0,
                RotX = 0,
                RotY = 0,
                RotZ = 0,
                ScaleX = 1f,
                ScaleY = 1f,
                ScaleZ = 1f,
                Red = 0,
                Green = 153,
                Blue = 0,
                Alpha = 100,
                BobUpAndDown = false,
                FaceCamera = false,
                P19 = 2,
                Rotate = false,
                TextureDict = null,
                TextureName = null,
                DrawOnEnts = false,
                UniqueID = $"BUILDING_MARKER{DbModel.Id}"
            };

            await MarkerHelper.CreateGlobalMarker(InteriorMarker);
            await MarkerHelper.CreateGlobalMarker(ExteriorMarker);

            // Później to zmienić i dodać do bazy może, ale to się zobaczy
            if (DbModel.BuildingType == BuildingType.Apartament) BlipVisable = false;


            if (BlipVisable)
            {
                string blipName = BuildingFactory.CreateName(DbModel.BuildingType);
                int blip = BuildingFactory.CreateBlip(DbModel.BuildingType, DbModel.OnSale);
                int color = BuildingFactory.CreateColor(DbModel.OnSale, DbModel.BuildingType);
                Blip = new BlipModel()
                {
                    PosX = DbModel.InternalPickupPositionX,
                    PosY = DbModel.InternalPickupPositionY,
                    PosZ = DbModel.InternalPickupPositionZ + 1,
                    Blip = blip,
                    Color = color, // Kolor pózniej do zmiany jak budynek będzie kupiony
                    Size = EBlipSize.Medium,
                    Name = blipName,
                    ShortRange = true,
                    UniqueID = $"BUILDING{DbModel.Id}"
                };
                Alt.Log("Tworze blipa z budynkiem");
                await BlipHelper.CreateGlobalBlip(Blip);

            }
            EntityHelper.Add(this);
            InteriorColshape.SetData("building:data", this);
            ExteriorColshape.SetData("building:data", this);

        });

        public BuildingEntity Create(Account creator, string name, BuildingType buildingType, Position internalPosition, Position externalPosition)
        {
            BuildingModel buildingToCreate = new BuildingModel()
            {
                Name = name,
                BuildingType = buildingType,
                EntryFee = 0,
                ExternalPickupPositionX = externalPosition.X,
                ExternalPickupPositionY = externalPosition.Y,
                ExternalPickupPositionZ = externalPosition.Z,
                InternalPickupPositionX = internalPosition.X,
                InternalPickupPositionY = internalPosition.Y,
                InternalPickupPositionZ = internalPosition.Z,
                MaxObjectsCount = 0,
                CurrentObjectsCount = 0,
                HasCCTV = false,
                HasSafe = false,
                Description = "",
                Creator = creator,
                CreatedTime = DateTime.Now,
                ItemsInBuilding = new List<ItemModel>()
            };

            Save();

            return new BuildingEntity(buildingToCreate);
        }

        public async Task UpdateBlip() => await AltAsync.Do(async () =>
        {
            if (!BlipVisable) return;

            string blipName = BuildingFactory.CreateName(DbModel.BuildingType);
            int blip = BuildingFactory.CreateBlip(DbModel.BuildingType, DbModel.OnSale);
            int color = BuildingFactory.CreateColor(DbModel.OnSale, DbModel.BuildingType);

            await BlipHelper.UpdateGlobalBlip($"BUILDING{DbModel.Id}", blip, blipName, color);
        });

        public bool IsCharacterOwner(IPlayer player)
        {
            return DbModel.CharacterId == player.GetAccountEntity().characterEntity.DbModel.Id ? true : false;
        }

        public void AddMoney(int amount)
        {
            DbModel.Balance += amount;
        }

        public void RemoveMoney(int amount)
        {
            DbModel.Balance -= amount;
        }

        public bool HasEnoughMoney(int amount)
        {
            return (DbModel.Balance >= amount) ? true : false;
        }

        public void AddTenantToBuilding(CharacterEntity characterEntity)
        {
            if (characterEntity.DbModel.Id == DbModel.CharacterId) return;

            if(DbModel.BuildingTenants.Any(plr => plr.CharacterId == characterEntity.DbModel.Id)) return;

            //Dodać postać itp
            BuildingTenantModel tenantToAdd = new BuildingTenantModel()
            {
                BuildingId = DbModel.Id,
                CharacterId = characterEntity.DbModel.Id,
                TenantAdded = DateTime.Now

            };

            DbModel.BuildingTenants.Add(tenantToAdd);
            Save();
        }

        public bool IsCharacterTenant(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.Any(x => x.CharacterId == characterEntity.DbModel.Id);
        }

        public bool CanPlayerEditBuilding(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanEditBuilding;
        }

        public bool CanPlayerWithdrawDeposit(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanWithdrawDeposit;
        }

        public bool CanPlayerManagmentTenants(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanManagmentTenants;
        }

        public bool CanPlayerManagmentMagazine(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanManagmentMagazine;
        }

        public bool CanPlayerRespawnInBuilding(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanRespawnInBuilding;
        }

        public bool CanPlayerLockDoor(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanLockDoor;
        }

        public bool CanPlayerManagmentGuests(CharacterEntity characterEntity)
        {
            return DbModel.BuildingTenants.FirstOrDefault(x => x.CharacterId == characterEntity.DbModel.Id).CanManagmentGuests;
        }

        public void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.BuildingRepository.Update(DbModel);
            }

            Alt.Log($"[BUILDING-ENTITY]: Zapisałem budynek: [{DbModel.Id} | {DbModel.Name}]");
            // Zrobić zapisów budynków
        }

        public static async Task LoadBuildingsAsync(UnitOfWork unit) => await AltAsync.Do(async () =>
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            foreach (BuildingModel building in await unit.BuildingRepository.GetAll())
            {
                Alt.Log($"[BUILDING-ENTITY: LOAD]: budynek o ID: {building.Id} został wczytany poprawnie!");
                BuildingEntity buildingEntity = new BuildingEntity(building);
                await buildingEntity.Spawn();
            }
        });

        public async Task Dispose() => await AltAsync.Do(async () =>
        {
            InteriorColshape.Remove();
            ExteriorColshape.Remove();
            await BlipHelper.DeleteGlobalBlip(Blip.UniqueID);
            //Dorobić usuwanie z bazy danych i usuwanie markerów
        });
    }
}
