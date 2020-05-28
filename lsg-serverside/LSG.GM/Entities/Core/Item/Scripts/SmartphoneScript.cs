using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class SmartphoneScript : IScript
    {
        [ClientEvent("item-smartphone:getSmartphone")]
        public void GetSmartphone(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if(characterEntity.CurrentSmartphone != null)
            {
                Smartphone smartphone = characterEntity.CurrentSmartphone;
                player.Emit("item-smartphone:getEnabledSmartphone",
                            smartphone.PhoneId,
                            smartphone.SmartphoneNumber,
                            smartphone.SmartphoneCredit,
                            smartphone.SmartphoneMemory,
                            smartphone.SmartphoneContacts,
                            smartphone.SmartphoneRecentCalls,
                            smartphone.SmartphoneMessages);
            }   
            else
            {

                player.SendChatMessageError("Musisz mieć wlączony telefon, aby móc go użyć");
            }
        }
    }
}
