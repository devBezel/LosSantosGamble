using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.VehicleModels
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int OwnerId { get; set; }
        public Character Owner { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotRoll { get; set; }
        public float RotPitch { get; set; }
        public float RotYaw { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public bool State { get; set; }
        public int Health { get; set; }

    }
}
