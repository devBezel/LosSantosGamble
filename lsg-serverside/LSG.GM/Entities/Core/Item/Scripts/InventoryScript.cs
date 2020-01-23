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
        public InventoryScript()
        {
            Alt.OnClient("inventory:getItems", InventoryGetItems);
        }
        public void InventoryGetItems(IPlayer player, object[] args)
        {
            List<ItemModel> items = player.GetAccountEntity().characterEntity.DbModel.Items.ToList();
            List<ItemEntity> usedItems = player.GetAccountEntity().characterEntity.ItemsInUse.ToList();

            //TODO: Wywala nulla przez co crashuje serwer, do naprawy
            player.Emit("inventory:items", JsonConvert.SerializeObject(items));
        }
    }
}
