using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Helpers.Models
{
    public class BlipModel : IWritable
    {
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public int Blip { get; set; }
        public int Color { get; set; }
        public float Size { get; set; }
        public string Name { get; set; }
        public bool ShortRange { get; set; }
        public string UniqueID { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("blip");
            writer.Value(Blip);

            writer.Name("color");
            writer.Value(Color);

            writer.Name("size");
            writer.Value(Size);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("shortRange");
            writer.Value(ShortRange);

            writer.Name("uniqueID");
            writer.Value(UniqueID);

            writer.EndObject();
        }
    }
}
