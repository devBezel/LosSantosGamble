using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Common.SmartphoneOpt
{
    public class SmartphoneIncomingCallModel : IWritable
    {
        public bool IsCalling { get; set; }
        public int CallNumber { get; set; }

        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("isCalling");
            writer.Value(IsCalling);

            writer.Name("callNumber");
            writer.Value(CallNumber);

            writer.EndObject();
        }
    }
}
