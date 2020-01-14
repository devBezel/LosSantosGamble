using AltV.Net;
using LSG.DAL.Database.Models.AccountModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.BusModels
{
    public class BusStop : IWritable
    {
        public int Id { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public int CreatorId { get; set; }

        public virtual Account Creator { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public List<BusStopStation> BusStopStations { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("creatorId");
            writer.Value(CreatorId);

            writer.Name("createdTime");
            writer.Value(CreatedTime.ToString());

            writer.EndObject();

        }

    }
}
