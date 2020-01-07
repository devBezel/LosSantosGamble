using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Vehicle
{
    public class VehicleToCharacterDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
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
