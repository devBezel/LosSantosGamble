using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Common.Shop;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class ShopExtenstion
    {
        public static ShopEntity GetShopEntity(this IColShape colshape)
        {
            colshape.GetData("shop:data", out ShopEntity shopEntity);
            return shopEntity;
        }
    }
}
