using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Core.Description;
using LSG.GM.Core.Login;
using System;

namespace LSG.GM
{
    public class Base : AsyncResource
    {
        public override void OnStart()
        {
            new LoginScript();
            new DescriptionScript();
            AltChat.SendBroadcast("halo");
        }

        public override void OnStop()
        {
            Alt.Log("Serwer wylączony");
        }
    }
}
