using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Offers
{
    public class OfferScript : IScript
    {
        #region ITEM: oferta przez ekwipunek

        [ClientEvent("inventory:offerPlayerItem")]
        public void InventoryOfferPlayerItem(IPlayer sender, string itemModelJson, IPlayer getter, int costItem)
        {
            ItemModel itemModel = JsonConvert.DeserializeObject<ItemModel>(itemModelJson);
            Alt.Log($"itemModel: {itemModel.Name}");
            //IPlayer getter = (IPlayer)args[1];
            //int costItem = Convert.ToInt32(args[2]);

            if (getter == null) return;

            if (sender == getter)
            {
                sender.SendErrorNotify("Wystąpił bląd", "Nie możesz zaoferować przedmiotu sam sobie");
                return;
            }

            if(Calculation.IsPlayerInRange(sender, getter, 3))
            {
                sender.SendChatMessageError("Tego gracza nie ma w pobliżu!");
                return;
            }

            CharacterEntity getterCharacterEntity = getter.GetAccountEntity().characterEntity;

            if (getterCharacterEntity.PendingOffer)
            {
                sender.SendChatMessageError("Ten gracz przeprowadza z kimś ofertę, spróbuj ponownie zachwile");
                return;
            }

            getter.GetAccountEntity().characterEntity.PendingOffer = true;
            getter.Emit("inventory:sendRequestOffer", itemModel, costItem, sender.GetAccountEntity().ServerID);
        }

        [ClientEvent("inventory:offerRequestResult")]
        public void InventoryOfferRequestResult(IPlayer getter, string itemModelJson, int costItem, int senderID, bool acceptOffer)
        {
            ItemModel itemModel = JsonConvert.DeserializeObject<ItemModel>(itemModelJson);
            IPlayer sender = PlayerExtenstion.GetPlayerById(senderID);

            if (sender == null)
                return;

            if (itemModel.ItemInUse)
            {
                sender.SendChatMessageError("Musisz odużyć przedmiot, aby móc go zaoferować");
                return;
            }

            CharacterEntity senderEntity = sender.GetAccountEntity().characterEntity;
            CharacterEntity getterEntity = getter.GetAccountEntity().characterEntity;
            Offer offer = new Offer(senderEntity, getterEntity, itemModel, costItem);

            if (acceptOffer)
            {
                //TODO: Zrobić mozliwość płacenia kartą
                offer.Trade(false);
                offer.Dispose();
            }
            else
            {
                offer.Dispose();
                sender.SendWarningNotify("Gracz odrzucił ofertę", "Twoja oferta została odrzucona");
            }

        }

        #endregion

        
    }
}
