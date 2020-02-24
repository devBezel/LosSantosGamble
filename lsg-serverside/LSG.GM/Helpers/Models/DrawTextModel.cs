using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Helpers.Models
{
    public class DrawTextModel : IWritable
    {
        public string Text { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public int Dimension { get; set; }
        public string UniqueID { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("text");
            writer.Value(Text);

            writer.Name("x");
            writer.Value(X);

            writer.Name("y");
            writer.Value(Y);

            writer.Name("z");
            writer.Value(Z);

            writer.Name("dimension");
            writer.Value(Dimension);

            writer.Name("uniqueID");
            writer.Value(UniqueID);

            writer.EndObject();
        }
    }
}
