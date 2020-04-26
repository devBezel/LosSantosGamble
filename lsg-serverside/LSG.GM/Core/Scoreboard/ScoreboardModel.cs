using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core.Scoreboard
{
    public class ScoreboardModel : IWritable
    {
        public int Id { get; set; }
        public string FormatName { get; set; }
        public float GamblePoints { get; set; }
        public uint Ping { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("formatName");
            writer.Value(FormatName);

            writer.Name("gamblePoints");
            writer.Value(GamblePoints);

            writer.Name("ping");
            writer.Value(Ping);

            writer.EndObject();
        }
    }
}
