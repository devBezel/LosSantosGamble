using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class InventoryScript : IScript
    {
        public static ItemEntityFactory ItemFactory { get; } = new ItemEntityFactory();


        //public InventoryScript()
        //{
        //    Alt.OnClient("inventory:getItems", InventoryGetItems);
        //    Alt.OnClient("inventory:useItem", InventoryUseItem);
        //    Alt.OnClient("inventory:offerPlayerItem", InventoryOfferPlayerItem);
        //    Alt.OnClient("inventory:offerRequestResult", InventoryOfferRequestResult);
        //}

        [ClientEvent("inventory:getItems")]
        public void InventoryGetItems(IPlayer player)
        {
            List<ItemModel> items = player.GetAccountEntity().characterEntity.DbModel.Items.ToList();
            List<ItemEntity> usedItems = player.GetAccountEntity().characterEntity.ItemsInUse.ToList();

            //TODO: Wywala nulla przez co crashuje serwer, do naprawy
            player.Emit("inventory:items", items);
        }

        [ClientEvent("inventory:useItem")]
        public void InventoryUseItem(IPlayer sender, int itemID)
        {
            //int itemID = (int)(long)args[0];

            Alt.Log("ItemID: " + itemID);
            

            CharacterEntity characterEntity = sender.GetAccountEntity().characterEntity;

            ItemEntity itemInUse = characterEntity.ItemsInUse.FirstOrDefault(x => x.Id == itemID);

            if (itemInUse != null)
            {
                itemInUse.UseItem(characterEntity);
                return;
            }

            ItemEntity itemEntity = ItemFactory.Create(characterEntity.DbModel.Items.FirstOrDefault(x => x.Id == itemID));
            itemEntity.UseItem(characterEntity);
        }
        //Do usunięcia pózniej
        [Command("createitem")]
        public void CreateItemCMD(IPlayer sender, int playerId, string name, double first, double second, double third, double fourth, int itemEntityType)
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(playerId);
            if (getter == null)
            {
                sender.SendErrorNotify(null, $"Gracz o ID {playerId} nie jest w grze");
                return;
            }
            ItemModel item = new ItemModel()
            {
                Name = name,
                CreatorId = sender.GetAccountEntity().DbModel.Id,
                FirstParameter = first,
                SecondParameter = second,
                ThirdParameter = third,
                FourthParameter = fourth,
                ItemEntityType = (ItemEntityType)itemEntityType,
                CharacterId = getter.GetAccountEntity().characterEntity.DbModel.Id,
                VehicleId = null,
                BuildingId = null,
                ItemInUse = false
            };
            ItemEntity itemEntity = ItemFactory.Create(item);
            itemEntity.Create(getter.GetAccountEntity().characterEntity);
        }

        [ClientEvent("inventory:offerPlayerItem")]
        public void InventoryOfferPlayerItem(IPlayer sender, string itemModelJson, IPlayer getter, int costItem)
        {
            ItemModel itemModel = JsonConvert.DeserializeObject<ItemModel>(itemModelJson);
            Alt.Log($"itemModel: {itemModel.Name}");
            //IPlayer getter = (IPlayer)args[1];
            //int costItem = Convert.ToInt32(args[2]);

            if (getter == null) return;

            if(sender == getter)
            {
                sender.SendErrorNotify("Wystąpił bląd", "Nie możesz zaoferować przedmiotu sam sobie");
                return;
            }

            getter.Emit("inventory:sendRequestOffer", itemModel, costItem, sender.GetAccountEntity().ServerID);
        }

        [ClientEvent("inventory:offerRequestResult")]
        public void InventoryOfferRequestResult(IPlayer getter, string itemModelJson, int costItem, int senderID, bool accept)
        {
            ItemModel itemModel = JsonConvert.DeserializeObject<ItemModel>(itemModelJson);
            //int costItem = Convert.ToInt32(args[1]);
            //int senderID = Convert.ToInt32(args[2]);
            //bool accept = (bool)args[3];

            IPlayer sender = PlayerExtenstion.GetPlayerById(senderID);

            if (sender == null) 
                return;

            if(!accept)
            {
                sender.SendWarningNotify("Gracz odrzucił ofertę", "Twoja oferta została odrzucona");
                return;
            }

            CharacterEntity senderEntity = sender.GetAccountEntity().characterEntity;
            CharacterEntity getterEntity = getter.GetAccountEntity().characterEntity;


            ItemEntity itemEntity = ItemFactory.Create(itemModel);

            itemEntity.Offer(senderEntity, getterEntity, costItem);
        }

    }
}
