using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Common.Shop
{
    public class ShopEntityFactory
    {

        public static string CreateShopName(ShopEntityType type)
        {
            switch (type)
            {
                case ShopEntityType.Market24: return "Sklep 24/7";
                case ShopEntityType.Gun: return "Sklep z bronią";
                default:
                    return "Nieznany sklep";
            }
        }
    }
}
