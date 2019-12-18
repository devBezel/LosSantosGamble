using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.VehicleModels;

namespace LSG.DAL.Database.Models.CharacterModels
{
    public class Character
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public string History { get; set; }
        public string PicUrl { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Rotation { get; set; }
        public float Money { get; set; }
        public float DirtyMoney { get; set; }
        public float Bank { get; set; }
        public bool BankStatus { get; set; }
        public float Health { get; set; }
        public float Armor { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<CharacterDescription> CharacterDescriptions { get; set; }
        public IEnumerable <CharacterDetail> CharacterDetails { get; set; }
    }
}
