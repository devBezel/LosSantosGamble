using AltV.Net;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.GroupModels
{
    public class GroupWorkerModel : IWritable
    {

        public int Id { get; set; }
        public int Salary { get; set; }
        public int DutyMinutes { get; set; }
        [EnumDataType(typeof(GroupRights))]
        public GroupRights Rights { get; set; }

        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int GroupRankId { get; set; }
        public GroupRankModel GroupRank { get; set; }



        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("salary");
            writer.Value(Salary);

            writer.Name("dutyMinutes");
            writer.Value(DutyMinutes);

            writer.Name("rights");
            writer.Value((int)Rights);

            writer.Name("groupId");
            writer.Value(GroupId);


            writer.Name("characterId");
            writer.Value(CharacterId);


            writer.Name("character");
            Character.OnWrite(writer);


            writer.Name("groupRankId");
            writer.Value(GroupRankId);

            writer.EndObject();
        }
    }
}
