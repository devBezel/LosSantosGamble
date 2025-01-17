﻿using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Enums;
using LSG.GM.Economy.Base.Jobs;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Economy.Jobs.Base.Junker;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Utilities;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

            ColShape = Alt.CreateColShapeCylinder(new Position(JobEntityModel.Position.X, JobEntityModel.Position.Y, JobEntityModel.Position.Z - 0.9f), 1f, 2f);

            JobEntityFactory entityFactory = new JobEntityFactory();
            Job = entityFactory.Create(JobEntityModel);

            Blip = new BlipModel()
            {
                PosX = JobEntityModel.Position.X,
                PosY = JobEntityModel.Position.Y,
                PosZ = JobEntityModel.Position.Z,
                Blip = entityFactory.CreateBlip(JobEntityModel.JobType),
                Color = entityFactory.CreateColor(JobEntityModel.JobType), 
                Size = EBlipSize.Medium,
                Name = $"{JobEntityModel.JobName} (Praca dorywcza)",
                ShortRange = true,
                UniqueID = $"JOB_BLIP{JobEntityModel.JobType}"
            };

            ColShape.SetData("job:data", this);
            EntityHelper.Add(this);

        }

        //TODO: Zrobić jeżeli minie dzień, że usuwa się JobEarned
        public void Start(CharacterEntity worker)
        {
            if (Calculation.CalculateTheNumberOfDays(DateTime.Now, worker.DbModel.JobEnded) >= 1)
            {
                worker.DbModel.JobEarned = 0;
            }

            if (worker.DbModel.JobEarned < JobEntityModel.MaxSalary)
            {
                if(JobEntityModel.RespawnVehicle)
                {
                    RespawnJobVehicle(worker);
                }

                worker.CasualJob = Job;
                worker.AccountEntity.Player.SendChatMessageToNearbyPlayers("wszedł szybkim krokiem do szatni i przebrał się w robocze ciuchy", ChatType.Me);
            }
            else
            {
                worker.AccountEntity.Player.SendChatMessageError("Wypracowałeś maksymalną płace w tej pracy, przyjdź jutro.");
            }

        }

        public void Stop(CharacterEntity worker)
        {
            if (worker.CasualJobVehicle != null)
            {
                DisposeJobVehicle(worker);
            }

            // Kończenie kuriera
            if(worker.CasualJob is CourierJob courierJob)
            {
                courierJob.Dispose(worker);
            }

            // Kończenie śmieciarza
            if(worker.CasualJob is JunkerJob junkerJob)
            {
                junkerJob.Dispose(worker);
            }

            worker.CasualJob = null;
            worker.AccountEntity.Player.SendChatMessageInfo($"Zarobiłeś {worker.DbModel.JobEarned}$ z pracy dorywczej.");

            
        }

        public void RespawnJobVehicle(CharacterEntity worker)
        {
            JobVehicleEntity jobVehicle = new JobVehicleEntity(JobEntityModel.VehicleModel, JobEntityModel.RespawnVehiclePosition, JobEntityModel.RespawnVehicleRotation, worker);

            CurrentVehiclesInWorld.Add(jobVehicle);
        }

        public void ChargeMoney(CharacterEntity worker, int money)
        {
            worker.AddMoney(money, false);
            worker.DbModel.JobEarned += money;

            if(worker.DbModel.JobEarned >= JobEntityModel.MaxSalary)
            {
                worker.DbModel.JobEnded = DateTime.Now;
            }
        }

        public void DisposeJobVehicle(CharacterEntity worker)
        {
            worker.CasualJobVehicle.Dispose();
            CurrentVehiclesInWorld.Remove(worker.CasualJobVehicle);
        }
    }

    public class JobEntityModel : IWritable
    {
        public string JobName { get; set; }
        public bool RespawnVehicle { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public Position Position { get; set; }
        public Position RespawnVehiclePosition { get; set; }
        public Position RespawnVehicleRotation { get; set; }
        public JobType JobType { get; set; }
        public int MaxSalary { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("jobName");
            writer.Value(JobName);

            writer.Name("jobType");
            writer.Value((int)JobType);

            writer.Name("maxSalary");
            writer.Value(MaxSalary);

            writer.EndObject();

        }
    }

}
