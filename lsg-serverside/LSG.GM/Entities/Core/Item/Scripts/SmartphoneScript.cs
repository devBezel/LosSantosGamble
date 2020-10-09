using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.DAL.Enums;
using LSG.GM.Entities.Common.SmartphoneOpt;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class SmartphoneScript : IScript
    {
        [ClientEvent("item-smartphone:getSmartphone")]
        public void GetSmartphone(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            Smartphone smartphone = characterEntity.CurrentSmartphone;

            if (smartphone != null)
            {
                SmartphoneCallModel smartphoneCallModel = smartphone.CurrentCall != null ? new SmartphoneCallModel() { CallerNumber = smartphone.CurrentCall.Caller.CurrentSmartphone.SmartphoneNumber,
                                                                                                                       GetterNumber = smartphone.CurrentCall.Receiving.CurrentSmartphone.SmartphoneNumber } : null;
                player.Emit("item-smartphone:getEnabledSmartphone",
                            smartphone.PhoneId,
                            smartphone.SmartphoneNumber,
                            smartphone.SmartphoneCredit,
                            smartphone.SmartphoneMemory,
                            smartphone.SmartphoneContacts,
                            smartphone.SmartphoneRecentCalls,
                            smartphone.SmartphoneMessages,
                            smartphoneCallModel,
                            smartphone.IncomingCall);
            }
            else
            {
                player.SendChatMessageError("Musisz mieć wlączony telefon, aby móc go użyć");
            }
        }

        [ClientEvent("item-smartphone:sendMessage")]
        public void SendMessage(IPlayer player, int smartphoneId, int getterNumber, string message)
        {
            CharacterEntity senderEntity = player.GetAccountEntity().characterEntity;
            if (senderEntity == null)
                return;

            if (senderEntity.CurrentSmartphone != null)
            {
                Smartphone senderSmartphone = senderEntity.CurrentSmartphone;
                senderSmartphone.SendMessage(senderEntity, smartphoneId, getterNumber, message);

                if (EntityHelper.GetCharacterBySmartphoneNumber(getterNumber) != null)
                {
                    CharacterEntity getterCharacterEntity = EntityHelper.GetCharacterBySmartphoneNumber(getterNumber);
                    getterCharacterEntity.AccountEntity.Player.Emit("item-smartphone:notify", (int)SmartphoneNotifyType.Message, message, senderEntity.CurrentSmartphone.SmartphoneNumber);
                }
            }
        }

        [ClientEvent("item-smartphone:call")]
        public void SmartphoneCallEvent(IPlayer caller, int getterNumber)
        {
            Alt.Log("Dzwonie do numeru " + getterNumber.ToString());

            CharacterEntity callerCharacterEntity = caller.GetAccountEntity().characterEntity;
            CharacterEntity receivingCharacterEntity = EntityHelper.GetCharacterBySmartphoneNumber(getterNumber);
            if (receivingCharacterEntity == null)
                return;

            Smartphone callerSmartphone = callerCharacterEntity.CurrentSmartphone;
            if (callerSmartphone == null)
                return;

            Smartphone receivingSmartphone = receivingCharacterEntity.CurrentSmartphone;
            if (receivingSmartphone == null)
            {
                //TODO: Event do callera, że gościu ma wylączony telefon
                return; 
            }

            Alt.Log("Wysylam dalszy emit");

            callerSmartphone.IncomingCall.CallNumber = getterNumber;
            callerSmartphone.IncomingCall.IsCalling = true;


            receivingSmartphone.IncomingCall.CallNumber = callerSmartphone.SmartphoneNumber;
            receivingSmartphone.IncomingCall.IsCalling = false;


            receivingCharacterEntity.AccountEntity.Player.Emit("item:smartphone:incomingCall", callerSmartphone.SmartphoneNumber, getterNumber);
        }

        [ClientEvent("item-smartphone:callReceive")]
        public void SmartphoneCallReceive(IPlayer caller, int getterNumber)
        {
            CharacterEntity callerCharacterEntity = caller.GetAccountEntity().characterEntity;
            CharacterEntity receivingCharacterEntity = EntityHelper.GetCharacterBySmartphoneNumber(getterNumber);
            if (receivingCharacterEntity == null)
                return;

            Smartphone callerSmartphone = callerCharacterEntity.CurrentSmartphone;
            if (callerSmartphone == null)
                return;

            Smartphone receivingSmartphone = receivingCharacterEntity.CurrentSmartphone;
            if (receivingSmartphone == null)
                return;

            callerSmartphone.IncomingCall = null;
            receivingSmartphone.IncomingCall = null;

            Timer callTimer = new Timer(5000);
            callTimer.Start();

            IVoiceChannel SmartphoneVoice = Alt.CreateVoiceChannel(false, 0);
            SmartphoneCall call = new SmartphoneCall(callerCharacterEntity, receivingCharacterEntity, callTimer, SmartphoneVoice);

            callTimer.Elapsed += (o, args) =>
            {
                call.CallTime += 5;
            };

            callerSmartphone.CurrentCall = call;
            receivingSmartphone.CurrentCall = call;
        }
    }
}
