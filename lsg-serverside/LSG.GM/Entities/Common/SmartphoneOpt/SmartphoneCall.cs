using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.SmartphoneModels;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Item;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Entities.Common.SmartphoneOpt
{
    public class SmartphoneCall
    {
        public CharacterEntity Caller { get; set; }
        public CharacterEntity Receiving { get; set; }

        public Timer CallTimer { get; set; }
        public float CallTime { get; set; }
        public IVoiceChannel VoiceChannel { get; set; }


        public SmartphoneCall(CharacterEntity caller, CharacterEntity receiving, Timer callTimer, IVoiceChannel voiceChannel)
        {
            Caller = caller;
            Receiving = receiving;
            CallTimer = callTimer;
            VoiceChannel = voiceChannel;
        }

        public void Call()
        {
            VoiceChannel.AddPlayer(Caller.AccountEntity.Player);
            VoiceChannel.AddPlayer(Receiving.AccountEntity.Player);

            Caller.CurrentSmartphone.IsTalking = true;
            Receiving.CurrentSmartphone.IsTalking = true;

        }

        public void Dispose()
        {

            CallTimer.Stop();
            CallTimer.Dispose();
            
            VoiceChannel.RemovePlayer(Caller.AccountEntity.Player);
            VoiceChannel.RemovePlayer(Receiving.AccountEntity.Player);

            Caller.CurrentSmartphone.IsTalking = false;
            Receiving.CurrentSmartphone.IsTalking = false;

            VoiceChannel.Remove();
        }
    }
}
