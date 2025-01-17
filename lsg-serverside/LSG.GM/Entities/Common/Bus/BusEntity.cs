﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Enums;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Common.Bus
{
    public class BusEntity
    {
        public IColShape ColShape { get; set; }
        public BusStop DbModel { get; set; }
        public BlipModel BlipModel { get; set; }
        public MarkerModel MarkerModel { get; set; }

        //public List<BusStopStation> BusStopStations = new List<BusStopStation>();

        public BusEntity(BusStop dbModel)
        {
            DbModel = dbModel;
        }

        public async Task CreateStation(BusStopStation busStopStation) => await AltAsync.Do(() =>
        {
            DbModel.BusStopStations.Add(busStopStation);

            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.BusRepository.Update(DbModel);
            }
        });

        public async Task Spawn(bool newBusStop = false) => await AltAsync.Do(async () =>
        {
            if (newBusStop)
                Save(true);

            ColShape = Alt.CreateColShapeCylinder(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ - 0.9f), 1f, 2f);

            MarkerModel = new MarkerModel()
            {
                Type = 22,
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
                UniqueID = $"BUS{DbModel.Id}"
            };

            BlipModel = new BlipModel()
            {
                PosX = DbModel.PosX,
                PosY = DbModel.PosY,
                PosZ = DbModel.PosZ + 1,
                Blip = 513,
                Color = 37,
                Size = EBlipSize.Medium,
                Name = "Przystanek",
                ShortRange = true,
                UniqueID = $"BUS{DbModel.Id}"
            };

            await MarkerHelper.CreateGlobalMarker(MarkerModel);
            await BlipHelper.CreateGlobalBlip(BlipModel);

            ColShape.SetData("bus:data", this);
            
            EntityHelper.Add(this);
        });
        
        public void Save(bool newBusStop = false)
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();

            using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                if (!newBusStop)
                    unitOfWork.BusRepository.Add(DbModel);
                else
                    unitOfWork.BusRepository.Update(DbModel);
            }
        }

        public static async Task LoadBusAsync(UnitOfWork unit)
        {
            foreach (BusStop busStop in await unit.BusRepository.GetAll())
            {
                Alt.Log($"[BUS-ENTITY: LOAD]: bus o ID: {busStop.Id} został wczytany poprawnie!");
                BusEntity busEntity = new BusEntity(busStop);
                await busEntity.Spawn();
            }
        }
    }
}
