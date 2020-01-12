using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Enums;
using LSG.GM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AtmModel = LSG.DAL.Database.Models.BankModels.Atm;

namespace LSG.GM.Entities.Common.Atm
{
    public class AtmEntity
    {
        public AtmModel DbModel { get; set; }
        public IColShape ColShape { get; set; }

        public AtmEntity(AtmModel model)
        {
            DbModel = model;
        }

        public void Spawn()
        {
            ColShape = Alt.CreateColShapeCylinder(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ), 1f, 2f);
            
            MarkerModel markerModel = new MarkerModel()
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
                UniqueID = $"MARKER{DbModel.Id}"
            };

            MarkerHelper.CreateMarker(markerModel);
            BlipHelper.CreateGlobalBlip(DbModel.PosX, DbModel.PosY, DbModel.PosZ + 1, 277, 25, EBlipSize.Medium, "ATM", 1.0f, $"ATM{DbModel.Id}");

            ColShape.SetData("atm:data", this);

            EntityHelper.Add(this);
        }


    }
}
