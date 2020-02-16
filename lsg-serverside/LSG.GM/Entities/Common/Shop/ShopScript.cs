using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
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

            new Interaction(player, "shop:openWindow", "aby otworzyć ~g~ sklep");
            player.SetData("current:shop", shopEntity);
        });


        private void OpenShopWindow(IPlayer player, object[] args)
        {
            Alt.Log("Otworzono okno sklepu");
        }


    }
}
