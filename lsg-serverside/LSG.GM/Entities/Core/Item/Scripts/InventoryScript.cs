using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.ItemModels;
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


        public InventoryScript()
        {
            Alt.OnClient("inventory:getItems", InventoryGetItems);
            Alt.OnClient("inventory:useItem", InventoryUseItem);
        }
        public void InventoryGetItems(IPlayer player, object[] args)
        {
            List<ItemModel> items = player.GetAccountEntity().characterEntity.DbModel.Items.ToList();
            List<ItemEntity> usedItems = player.GetAccountEntity().characterEntity.ItemsInUse.ToList();

            //TODO: Wywala nulla przez co crashuje serwer, do naprawy
            player.Emit("inventory:items", items);
        }

        public void InventoryUseItem(IPlayer sender, object[] args)
        {
            int itemID = (int)(long)args[0];

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
    }
}
