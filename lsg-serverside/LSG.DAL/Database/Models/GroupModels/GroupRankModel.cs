using AltV.Net;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.GroupModels
{
    public class GroupRankModel : IWritable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(GroupRights))]
        public GroupRights Rights { get; set; }

        public int Salary { get; set; }

        public int GroupId { get; set; }
        public virtual GroupModel Group { get; set; }

        public int? DefaultForGroupId { get; set; }
        public virtual GroupModel DefaultForGroup { get; set; }
        // navigation properties
        public virtual ICollection<GroupWorkerModel> Workers { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("rights");
            writer.Value((int)Rights);

            writer.Name("salary");
            writer.Value(Salary);

            writer.Name("groupId");
            writer.Value(GroupId);

            writer.Name("defaultForGroupId");
            if(DefaultForGroupId.HasValue)
            {
                writer.Value(DefaultForGroupId.Value);
            }

            writer.EndObject();
        }
    }
}
