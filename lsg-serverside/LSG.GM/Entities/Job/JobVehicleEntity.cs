using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Vehicle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VehicleDataModel = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Economy.Base.Jobs
{
    public class JobVehicleEntity
    {
        public VehicleEntity VehicleEntity { get; set; }
        public CharacterEntity Worker { get; set; }


        public JobVehicleEntity(VehicleModel vehicleModel, Position respawnPosition, Rotation rotation, CharacterEntity worker)
        {

            Worker = worker;

            VehicleEntity = new VehicleEntity(new VehicleDataModel()
            {
                Id = 600,
                Model = vehicleModel.ToString(),
                Owner = worker.DbModel,
                OwnerId = worker.DbModel.Id,
                Group = null,
                PosX = respawnPosition.X,
                PosY = respawnPosition.Y,
                PosZ = respawnPosition.Z,
                RotPitch = rotation.Pitch,
                RotRoll = rotation.Roll,
                RotYaw = rotation.Yaw,
                R = 255,
                G = 255,
                B = 255,
                State = true,
                Health = 1000
            }, true);


            VehicleEntity.Spawn();

            Worker.CasualJobVehicle = this;
        }

        public void Dispose()
        {
            Worker.CasualJobVehicle = null;
            VehicleEntity.Dispose();
        }
    }
}
