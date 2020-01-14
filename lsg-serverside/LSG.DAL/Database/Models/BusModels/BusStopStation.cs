using AltV.Net;
using LSG.DAL.Database.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.BusModels
{
    public class BusStopStation : IWritable
    {
        public int Id { get; set; }

        public int BusStopId { get; set; }
        public virtual BusStop BusStop { get; set; }

        public string Name { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public float Time { get; set; }
        public int Cost { get; set; }

        public int CreatorId { get; set; }
        public virtual Account Creator { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("busStopId");
            writer.Value(BusStopId);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("time");
            writer.Value(Time);

            writer.Name("cost");
            writer.Value(Cost);

            writer.Name("creatorId");
            writer.Value(CreatorId);

            writer.Name("createdTime");
            writer.Value(CreatedTime.ToString());

            writer.EndObject();

        }
    }
}
