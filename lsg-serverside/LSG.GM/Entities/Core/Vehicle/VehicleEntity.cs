using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Repositories;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Base;
using LSG.GM.Entities.Base.Interfaces;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;
using Newtonsoft.Json;
using System.Linq;
//using LSG.GM.Entities.Admin;
using LSG.GM.Extensions;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleEntity : GameEntity
    {
        public IVehicle GameVehicle { get; set; }
        public VehicleDb DbModel { get; set; }
        private bool _nonDbVehicle;

        public VehicleEntity(VehicleDb model)
        {
            DbModel = model;
        }

        public static VehicleEntity Create(Position position, VehicleModel model, string numberPlate, int numberPlateStyle, Color color, Color sedondaryColor, Character character)
        {
            VehicleDb vehicle = new VehicleDb()
            {
                Owner = character,
                Model = model.ToString(),
                PosX = position.X,
                PosY = position.Y,
                PosZ = position.Z,
                R = color.R,
                G = color.G,
                B = color.B,
                Health = 1000
            };

            bool nonDbVehicle = character == null;


            if(!nonDbVehicle)
            {
                RoleplayContext ctx = Singleton.GetDatabaseInstance();
                using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
                {
                    unitOfWork.VehicleRepository.Add(vehicle);
                }
            }


            return new VehicleEntity(vehicle)
            {
                _nonDbVehicle = nonDbVehicle
            };

        }

        public void ChangeSpawnPosition()
        {
            DbModel.PosX = GameVehicle.Position.X;
            DbModel.PosY = GameVehicle.Position.Y;
            DbModel.PosZ = GameVehicle.Position.Z;
            DbModel.RotPitch = GameVehicle.Rotation.Pitch;
            DbModel.RotRoll = GameVehicle.Rotation.Roll;
            DbModel.RotYaw = GameVehicle.Rotation.Yaw;

            
            if (_nonDbVehicle) return;

            Save();
        }

        public void Save()
        {
            DbModel.Health = GameVehicle.EngineHealth;
            DbModel.PosX = GameVehicle.Position.X;
            DbModel.PosY = GameVehicle.Position.Y;
            DbModel.PosZ = GameVehicle.Position.Z;
            DbModel.RotPitch = GameVehicle.Rotation.Pitch;
            DbModel.RotRoll = GameVehicle.Rotation.Roll;
            DbModel.RotYaw = GameVehicle.Rotation.Yaw;

            DbModel.R = GameVehicle.PrimaryColorRgb.R;
            DbModel.G = GameVehicle.PrimaryColorRgb.G;
            DbModel.B = GameVehicle.PrimaryColorRgb.B;

            if (_nonDbVehicle) return;

            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.VehicleRepository.Update(DbModel);
            }
        }

        public int IncrementID
        {
            get
            {
                GameVehicle.GetData("vehicle:incrementId", out int result);

                return result;
            }
        }

        public override void Dispose()
        {
            if (!_nonDbVehicle) Save();

            GameVehicle.Remove();
        }
        
        public override void Spawn(IPlayer player)
        {
            IEnumerable<IVehicle> veh = Alt.GetAllVehicles().Where(v => v.GetData("vehicle:data", out VehicleEntity vehicleData) && vehicleData.DbModel.Owner.Id == player.GetAccountEntity().characterEntity.DbModel.Id);
            GameVehicle = Alt.CreateVehicle(DbModel.Model.ToString(), new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ), new Rotation(0, 0, 0));

            Alt.Log($"{veh.Count()} ilosc pojazdow gracza");
            Alt.Log("z Spawn()" + DbModel.Id.ToString());
            GameVehicle.SetData("vehicle:data", this);
            GameVehicle.SetData("vehicle:id", DbModel.Id);
            GameVehicle.SetData("vehicle:incrementId", veh.Count() + 1);

            Save();
        }

    }
}
