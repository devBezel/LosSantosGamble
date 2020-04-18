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
    public class VehicleEntity  /*GameEntity*/
    {
        public IVehicle GameVehicle { get; set; }
        public VehicleDb DbModel { get; set; }
        private bool _nonDbVehicle;

        public bool TrunkOpen { get; set; } = false;

        public VehicleEntity(VehicleDb model)
        {
            DbModel = model;
        }

        public static VehicleEntity Create(Position position, VehicleModel model, Color color, Color sedondaryColor, Character character)
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

        public bool IsGroupVehicle
        {
            get
            {
                return DbModel.GroupId != null ? true : false;
            }
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
            Alt.Log("Zapisuje");
            DbModel.Health = GameVehicle.EngineHealth;
            DbModel.PosX = GameVehicle.Position.X;
            DbModel.PosY = GameVehicle.Position.Y;
            DbModel.PosZ = GameVehicle.Position.Z;
            DbModel.RotPitch = GameVehicle.Rotation.Pitch;
            DbModel.RotRoll = GameVehicle.Rotation.Roll;
            DbModel.RotYaw = GameVehicle.Rotation.Yaw;

            Alt.Log($"{GameVehicle.Rotation.Pitch}  {GameVehicle.Rotation.Roll} {GameVehicle.Rotation.Yaw} ");

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

        public void Dispose(IPlayer player)
        {
            if (!_nonDbVehicle) Save();

            GameVehicle.Remove();
            player.GetAccountEntity().characterEntity.RespawnVehicleCount--;
        }

        public void Spawn(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            GameVehicle = Alt.CreateVehicle(DbModel.Model.ToString(), new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ), new Rotation(DbModel.RotPitch, DbModel.RotPitch, DbModel.RotYaw));

            GameVehicle.PrimaryColorRgb = new Rgba((byte)DbModel.R, (byte)DbModel.G, (byte)DbModel.B, 1);
            GameVehicle.SetWheels(1, 2);
            GameVehicle.NumberplateText = $"LS {DbModel.Id}";
            GameVehicle.EngineOn = false;
            GameVehicle.ManualEngineControl = true;

            GameVehicle.SetData("vehicle:data", this);
            GameVehicle.SetData("vehicle:id", DbModel.Id);
            GameVehicle.SetSyncedMetaData("vehicle:syncedData", DbModel);

            if (!IsGroupVehicle)
            {
                characterEntity.RespawnVehicleCount++;
                //Alt.Log("Nie jest grupowym pojazdem");

                //IEnumerable<IVehicle> veh = Alt.GetAllVehicles().Where(v => v.GetData("vehicle:data", out VehicleEntity vehicleData) && vehicleData.DbModel.Owner.Id == player.GetAccountEntity().characterEntity.DbModel.Id);
                //Alt.Log($"VEH COUNT: {veh.Count()}");
                //if(veh != null)
                //{
                //    GameVehicle.SetData("vehicle:incrementId", veh.Count() + 1);
                //}
            }

            //Save();
        }

    }
}
