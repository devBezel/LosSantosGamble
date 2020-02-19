using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.ShopModels;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Item.Scripts;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Common.Shop
{
    public class ShopScript : IScript
    {
        public ShopScript()
        {
            AltAsync.OnColShape += OnEnterColshape;
            Alt.OnClient("shop:openWindow", OpenShopWindow);
            Alt.OnClient("shop:buyItem", BuyItemInShop);
        }

        private async Task OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;
            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;

            AltAsync.Log("Shop");

            ShopEntity shopEntity = colShape.GetShopEntity();

            if (shopEntity == null || shopEntity.ColShape != colShape) return;

            IPlayer player = targetEntity as IPlayer;

            if (player.IsInVehicle) return;

            player.SetData("current:shop", shopEntity);
            new Interaction(player, "shop:openWindow", "aby otworzyć ~g~sklep");
        });


        private void OpenShopWindow(IPlayer player, object[] args)
        {
            Alt.Log("otwieram okno");
            player.GetData("current:shop", out ShopEntity shopEntity);

            player.Emit("shop:data", shopEntity.DbModel.ShopAssortments);


            player.DeleteData("current:shop");
        }

        private void BuyItemInShop(IPlayer player, object[] args)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            int countToBuy = (int)(long)args[0];
            if (countToBuy == 0) return;

            ShopAssortmentModel itemObject = JsonConvert.DeserializeObject<ShopAssortmentModel>(args[1].ToString());
            int itemCountCalculate = itemObject.Count * countToBuy;
            int itemCostCalculate = itemObject.Cost * countToBuy;

            if(!characterEntity.HasEnoughMoney(itemCostCalculate))
            {
                player.SendErrorNotify("Nie posiadasz tylu pieniędzy", "Udaj się do banku i wypłać pieniądze, aby móc kupić ten przedmiot");
                return;
            }

            
            ItemModel itemToCreate = new ItemModel()
            {
                Name = itemObject.Name, 
                FirstParameter = itemObject.FirstParameter,
                ThirdParameter = itemObject.ThirdParameter,
                FourthParameter = itemObject.FourthParameter,
                ItemEntityType = itemObject.ItemEntityType,
                Count = itemCountCalculate,
                CharacterId = characterEntity.DbModel.Id
            };

            characterEntity.RemoveMoney(itemCostCalculate);
            player.SendSuccessNotify($"Kupiłeś {itemCountCalculate}x {itemObject.Name.ToLower()}", "Zakupione itemy znajdują się w twoim ekwipunku");

            ItemEntity itemEntity = InventoryScript.ItemFactory.Create(itemToCreate);
            itemEntity.Create(characterEntity);


        }

    }
}
