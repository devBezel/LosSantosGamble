using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Core.VoiceChat
{
    public class VoiceChatScript : IScript
    {
        IVoiceChannel GlobalVoice = Alt.CreateVoiceChannel(true, 50);
        IVoiceChannel MonoVoice = Alt.CreateVoiceChannel(false, 0);

        public VoiceChatScript()
        {
            AltAsync.OnPlayerConnect += OnPlayerConnectAsync;
        }

        private async Task OnPlayerConnectAsync(IPlayer player, string reason) => await AltAsync.Do(() =>
        {
            GlobalVoice.AddPlayer(player);
        });
    }
}
