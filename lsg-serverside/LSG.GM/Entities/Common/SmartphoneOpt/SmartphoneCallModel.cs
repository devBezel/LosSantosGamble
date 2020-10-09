using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Common.SmartphoneOpt
{
    public class SmartphoneCallModel : IWritable
    {
        public int CallerNumber { get; set; }
        public int GetterNumber { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("callerNumber");
            writer.Value(CallerNumber);

            writer.Name("getterNumber");
            writer.Value(GetterNumber);

            writer.EndObject();
        }
    }
}
