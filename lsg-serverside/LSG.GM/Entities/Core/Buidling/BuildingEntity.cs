using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using System;
using System.Collections.Generic;
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
            InteriorColshape = Alt.CreateColShapeCylinder(new Position(DbModel.InternalPickupPositionX, DbModel.InternalPickupPositionY, DbModel.InternalPickupPositionZ), 1f, 2f);
            ExteriorColshape = Alt.CreateColShapeCylinder(new Position(DbModel.ExternalPickupPositionX, DbModel.ExternalPickupPositionY, DbModel.ExternalPickupPositionZ), 1f, 2f);
            ExteriorColshape.Dimension = DbModel.Id;

            InteriorMarker = new MarkerModel()
            {
                Type = 27,
                PosX = DbModel.InternalPickupPositionX,
                PosY = DbModel.InternalPickupPositionY,
                PosZ = DbModel.InternalPickupPositionZ,
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
                Type = 27,
                PosX = DbModel.ExternalPickupPositionX,
                PosY = DbModel.ExternalPickupPositionY,
                PosZ = DbModel.ExternalPickupPositionZ,
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


            if (BlipVisable)
            {
                string blipName = BuildingFactory.CreateName(DbModel.BuildingType);
                int blip = BuildingFactory.CreateBlip(DbModel.BuildingType, DbModel.OnSale);
                Blip = new BlipModel()
                {
                    PosX = DbModel.InternalPickupPositionX,
                    PosY = DbModel.InternalPickupPositionY,
                    PosZ = DbModel.InternalPickupPositionZ + 1,
                    Blip = blip,
                    Color = 37, // Kolor pózniej do zmiany jak budynek będzie kupiony
                    Size = EBlipSize.Medium,
                    Name = blipName,
                    ShortRange = 1.0f,
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

        public bool IsCharacterOwner(IPlayer player)
        {
            return DbModel.CharacterId == player.GetAccountEntity().characterEntity.DbModel.Id ? true : false;
        }

        public void Save()
        {
            // Zrobić zapisów budynków
        }

        public async Task Dispose() => await AltAsync.Do(async () =>
        {
            InteriorColshape.Remove();
            ExteriorColshape.Remove();
            await BlipHelper.DeleteGlobalBlip(Blip.UniqueID);
            //Dorobić usuwanie z bazy danych i usuwanie markerów
        });
    }
}
