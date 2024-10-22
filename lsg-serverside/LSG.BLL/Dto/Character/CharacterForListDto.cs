﻿using System;
using System.Collections.Generic;
using System.Text;
using AltV.Net;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Vehicle;
using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.CharacterModels;
using Newtonsoft.Json;

namespace LSG.BLL.Dto.Character
{
    public class CharacterForListDto : IWritable
    {
        public int Id { get; set; }
        public AccountForCharacterDto Account { get; set; }
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
        public IEnumerable<VehicleToCharacterDto> Vehicles { get; set; }
        public IEnumerable<CharacterDescriptionForScriptDto> CharacterDescriptions { get; set; }
        public CharacterLookDto CharacterLook { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("surname");
            writer.Value(Surname);

            writer.Name("age");
            writer.Value(Age);

            writer.Name("gender");
            writer.Value(Gender);

            writer.Name("height");
            writer.Value(Height);

            writer.Name("weight");
            writer.Value(Weight);

            writer.Name("description");
            writer.Value(Description);

            writer.Name("history");
            writer.Value(History);

            writer.Name("picUrl");
            writer.Value(PicUrl);

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("rotation");
            writer.Value(Rotation);

            //writer.Name("group_1");
            //writer.Value(group_1);

            //writer.Name("group_2");
            //writer.Value(group_2);

            writer.Name("money");
            writer.Value(Money);

            writer.Name("dirtymoney");
            writer.Value(DirtyMoney);

            writer.Name("bank");
            writer.Value(Bank);

            writer.Name("health");
            writer.Value(Health);

            writer.Name("armor");
            writer.Value(Armor);

            writer.Name("vehicles");
            writer.Value(JsonConvert.SerializeObject(Vehicles));

            writer.Name("characterDescriptions");
            writer.Value(JsonConvert.SerializeObject(CharacterDescriptions));

            writer.Name("characterLook");
            writer.Value(JsonConvert.SerializeObject(CharacterLook));

            writer.EndObject();

        }
    }
}
