using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Helpers
{
    public static class MarkerHelper
    {
        public static void CreateMarker(MarkerModel marker)
        {
            Alt.EmitAllClients("marker:create", marker);
        }
    }
}
