using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Enums;
using LSG.GM.Economy.Base.Jobs;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Enums;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Job
{
    public class JobEntity
    {
        public List<JobVehicleEntity> CurrentVehiclesInWorld = new List<JobVehicleEntity>();

        public JobEntityModel JobEntityModel { get; set; }
        public MarkerModel Marker { get; set; }
        public BlipModel Blip { get; set; }
        public IColShape ColShape { get; set; }

        public JobEntity Job { get; set; }

        public JobEntity(JobEntityModel jobEnityModel)
        {
            JobEntityModel = jobEnityModel;
        }

        public void Create()
        {
            Marker = new MarkerModel()
            {
                Type = 1,
                Dimension = 0,
                PosX = JobEntityModel.Position.X,
                PosY = JobEntityModel.Position.Y,
                PosZ = JobEntityModel.Position.Z - 0.9f,
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
                UniqueID = $"JOB_MARKER{JobEntityModel.JobType}"
            };

            Blip = new BlipModel()
            {
                PosX = JobEntityModel.Position.X,
                PosY = JobEntityModel.Position.Y,
                PosZ = JobEntityModel.Position.Z,
                Blip = 616,
                Color = 52, // Kolor pózniej do zmiany jak budynek będzie kupiony
                Size = EBlipSize.Medium,
                Name = "LS Kurier (Praca dorywcza)",
                ShortRange = true,
                UniqueID = $"JOB_BLIP{JobEntityModel.JobType}"
            };

            ColShape = Alt.CreateColShapeCylinder(new Position(JobEntityModel.Position.X, JobEntityModel.Position.Y, JobEntityModel.Position.Z - 0.9f), 1f, 2f);

            JobEntityFactory entityFactory = new JobEntityFactory();
            Job = entityFactory.Create(JobEntityModel);

            ColShape.SetData("job:data", this);
            EntityHelper.Add(this);

            
        }



        public void RespawnJobVehicle(CharacterEntity worker)
        {
            JobVehicleEntity jobVehicle = new JobVehicleEntity(JobEntityModel.VehicleModel, JobEntityModel.RespawnVehiclePosition, JobEntityModel.RespawnVehicleRotation, worker);

            CurrentVehiclesInWorld.Add(jobVehicle);
        }

        public void DisposeJobVehicle(CharacterEntity worker)
        {
            if (worker.CasualJobVehicle != null)
            {
                worker.CasualJobVehicle.Dispose();
                CurrentVehiclesInWorld.Remove(worker.CasualJobVehicle);
            }
        }
    }

    public class JobEntityModel
    {
        public string JobName { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public Position Position { get; set; }
        public Position RespawnVehiclePosition { get; set; }
        public Position RespawnVehicleRotation { get; set; }
        public JobType JobType { get; set; }
    }

}
