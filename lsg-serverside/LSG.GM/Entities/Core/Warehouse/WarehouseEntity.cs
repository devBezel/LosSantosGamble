using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Warehouse
{
    public class WarehouseEntity
    {
        public GroupEntity GroupEntity { get; set; }
        public WarehouseModel DbModel { get; set; }
        public IColShape ColShape { get; set; }
        public MarkerModel Marker { get; set; }

        public WarehouseEntity(GroupEntity group, WarehouseModel dbModel)
        {
            GroupEntity = group;
            DbModel = dbModel;
        }

        public void Spawn()
        {
            Alt.Log($"[WAREHOUSE-ENTITY] Warehouse dla grupy {GroupEntity.DbModel.Id} został stworzony");

            ColShape = Alt.CreateColShapeCylinder(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ - 0.9f), 5f, 2f);
            ColShape.SetData("warehouse:data", this);


            Marker = new MarkerModel()
            {
                Type = 1,
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
                UniqueID = $"WAREHOUSE_MARKER{DbModel.Id}"
            };

            // Do testów później nie będzie
            AltAsync.Do(async () =>
            {
                await MarkerHelper.CreateGlobalMarker(Marker);
            });

            EntityHelper.Add(this);
        }

        public void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.WarehouseRepository.Update(DbModel);
            }
        }
    }
}
