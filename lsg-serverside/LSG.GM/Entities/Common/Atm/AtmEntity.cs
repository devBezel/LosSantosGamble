using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.UnitOfWork;
using LSG.GM.Enums;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtmModel = LSG.DAL.Database.Models.BankModels.Atm;

namespace LSG.GM.Entities.Common.Atm
{
    public class AtmEntity
    {
        public AtmModel DbModel { get; set; }
        public IColShape ColShape { get; set; }
        public MarkerModel MarkerModel { get; set; }
        public BlipModel BlipModel { get; set; }

        public AtmEntity(AtmModel model)
        {
            DbModel = model;
        }

        public async Task Spawn(bool respawnNew = false) => await AltAsync.Do(async () =>
        {
            if (respawnNew)
                Save(true);

            ColShape = Alt.CreateColShapeCylinder(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ), 1f, 2f);

            MarkerModel = new MarkerModel()
            {
                Type = 27,
                PosX = DbModel.PosX,
                PosY = DbModel.PosY,
                PosZ = DbModel.PosZ,
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
                UniqueID = $"ATM_MARKER{DbModel.Id}"
            };

            BlipModel = new BlipModel()
            {
                PosX = DbModel.PosX,
                PosY = DbModel.PosY,
                PosZ = DbModel.PosZ + 1,
                Blip = 277,
                Color = 25,
                Size = EBlipSize.Medium,
                Name = "ATM",
                ShortRange = 1.0f,
                UniqueID = $"ATM{DbModel.Id}"
            };

            await MarkerHelper.CreateGlobalMarker(MarkerModel);
            await BlipHelper.CreateGlobalBlip(BlipModel);

            ColShape.SetData("atm:data", this);

            EntityHelper.Add(this);
        });

        public void Save(bool newAtm = false)
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();

            using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                if (!newAtm)
                    unitOfWork.AtmRepository.Update(DbModel);
                else
                    unitOfWork.AtmRepository.Add(DbModel);
            }
        }


    }
}
