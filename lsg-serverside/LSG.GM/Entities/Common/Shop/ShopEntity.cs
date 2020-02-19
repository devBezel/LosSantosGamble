using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Enums;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Common.Shop
{
    public class ShopEntity
    {
        public IColShape ColShape { get; set; }
        public ShopModel DbModel { get; set; }
        public BlipModel BlipModel { get; set; }
        public MarkerModel MarkerModel { get; set; }

        public ShopEntity(ShopModel dbModel)
        {
            DbModel = dbModel;
        }

        public async Task Spawn() => await AltAsync.Do(async () =>
        {
            ColShape = Alt.CreateColShapeCylinder(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ - 0.9f), 1f, 2f);

            MarkerModel = new MarkerModel()
            {
                Type = 27,
                Dimension = 0,
                PosX = DbModel.PosX,
                PosY = DbModel.PosY,
                PosZ = DbModel.PosZ - 0.9f,
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
                UniqueID = $"SHOP{DbModel.Id}"
            };

            BlipModel = new BlipModel()
            {
                PosX = DbModel.PosX,
                PosY = DbModel.PosY,
                PosZ = DbModel.PosZ + 1,
                Blip = 52,
                Color = 38,
                Size = EBlipSize.Medium,
                Name = ShopEntityFactory.CreateShopName(DbModel.ShopEntityType),
                ShortRange = true,
                UniqueID = $"SHOP{DbModel.Id}"
            };

            await MarkerHelper.CreateGlobalMarker(MarkerModel);
            await BlipHelper.CreateGlobalBlip(BlipModel);
            EntityHelper.Add(this);
            ColShape.SetData("shop:data", this);
        });

        public static async Task LoadShopAsync(UnitOfWork unit) => await AltAsync.Do(async () =>
        {
            foreach (ShopModel shop in await unit.ShopRepository.GetAll())
            {
                ShopEntity shopEntity = new ShopEntity(shop);
                await shopEntity.Spawn();
            }
        });
    }
}
