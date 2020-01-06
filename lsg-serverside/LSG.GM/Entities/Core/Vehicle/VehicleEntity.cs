using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Repositories;
using LSG.DAL.UnitOfWork;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Entities.Core.Vehicle
{
    public class VehicleEntity
    {
        public IVehicle GameVehicle { get; set; }
        public VehicleDb DbModel { get; set; }

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



        private bool _nonDbVehicle;
    }
}
