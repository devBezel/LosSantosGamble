using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Economy.Offers
{
    public class OfferScript : IScript
    {
        #region ITEM: oferta przez ekwipunek

        [ClientEvent("inventory:offerPlayerItem")]
        public void InventoryOfferPlayerItem(IPlayer sender, int itemID, IPlayer getter, int costItem)
        {
            CharacterEntity characterEntitySender = sender.GetAccountEntity().characterEntity;
            if (characterEntitySender == null) return;

            ItemModel itemToOffer = characterEntitySender.DbModel.Items.Find(x => x.Id == itemID);

            if (getter == null) return;

            if (sender == getter)
            {
                sender.SendErrorNotify("Wystąpił bląd", "Nie możesz zaoferować przedmiotu sam sobie");
                return;
            }

            if(!Calculation.IsPlayerInRange(sender, getter, 5))
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
            getter.Emit("inventory:sendRequestOffer", itemToOffer, costItem, sender.GetAccountEntity().ServerID);
        }

        [ClientEvent("inventory:offerRequestResult")]
        public void InventoryOfferRequestResult(IPlayer getter, int itemID, int costItem, int senderID, bool acceptOffer)
        {            
            IPlayer sender = PlayerExtenstion.GetPlayerById(senderID);

            if (sender == null)
                return;

            ItemModel itemToOffer = sender.GetAccountEntity().characterEntity.DbModel.Items.First(x => x.Id == itemID);
            if (itemToOffer == null) return;


            if (itemToOffer.ItemInUse)
            {
                sender.SendChatMessageError("Musisz odużyć przedmiot, aby móc go zaoferować");
                return;
            }

            CharacterEntity senderEntity = sender.GetAccountEntity().characterEntity;
            CharacterEntity getterEntity = getter.GetAccountEntity().characterEntity;

            
            Offer offer = new Offer(senderEntity, getterEntity, itemToOffer, costItem);

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

        [Command("oitem")]
        public void OfferItemCMD(IPlayer sender, int getterId, int itemId, int money)
        {
            CharacterEntity characterEntitySender = sender.GetAccountEntity().characterEntity;
            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if (getter == null)
            {
                sender.SendChatMessageError("Gracza o podanym ID nie ma w grze");
                return;
            }

            if (sender == getter)
            {
                sender.SendChatMessageError("Nie możesz zaoferować przedmiotu sam sobie");
                return;
            }

            CharacterEntity characterEntityGetter = getter.GetAccountEntity().characterEntity;
            ItemModel itemModel = characterEntitySender.DbModel.Items.First(x => x.Id == itemId);

            if (itemModel == null)
                return;

            if (itemModel.ItemInUse)
            {
                sender.SendChatMessageError("Musisz odużyć przedmiot, aby móc go zaoferować");
                return;
            }

            Offer offer = new Offer(characterEntitySender, characterEntityGetter, itemModel, money);
            offer.Trade(false);

        }

        #endregion


    }
}
