using AltV.Net;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.VehicleModels;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.GroupModels
{
    public class GroupModel : IWritable
    {
        //public GroupModel()
        //{
        //    Workers = new HashSet<GroupWorkerModel>();
        //    Ranks = new HashSet<GroupRankModel>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Grant { get; set; }
        public int MaxPayday { get; set; }

        public int Money { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [EnumDataType(typeof(GroupType))]
        public GroupType GroupType { get; set; }

        public int CreatorId { get; set; }
        public Character Creator { get; set; }

        public int LeaderId { get; set; }
        public Character Leader { get; set; }

        [ForeignKey("DefaultRank")]
        public int DefaultRankId { get; set; }
        public virtual GroupRankModel DefaultRank { get; set; }

        public List<GroupWorkerModel> Workers { get; set; }
        public List<GroupRankModel> Ranks { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<WarehouseModel> Warehouses { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("tag");
            writer.Value(Tag);

            writer.Name("grant");
            writer.Value(Grant);

            writer.Name("maxPayday");
            writer.Value(MaxPayday);

            writer.Name("money");
            writer.Value(Money);


            writer.Name("created");
            writer.Value(Created.ToString());

            writer.Name("groupType");
            writer.Value((int)GroupType);

            writer.Name("creatorId");
            writer.Value(CreatorId);

            writer.Name("leaderId");
            writer.Value(LeaderId);

            writer.EndObject();
        }


    }
}
