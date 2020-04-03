using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;
using static LSG.GM.Entities.Base.Interfaces.IEntityFactory;

namespace LSG.GM.Entities.Core.Item
{
    public class ItemEntityFactory : IEntityFactory<ItemEntity, ItemModel>
    {
        public ItemEntity Create(ItemModel item)
        {
            switch (item.ItemEntityType)
            {
                case DAL.Enums.ItemEntityType.Weapon: return new Weapon(item);
                case DAL.Enums.ItemEntityType.WeaponHolster: return new WeaponHolster(item);
                case DAL.Enums.ItemEntityType.Food: return new Food(item);
                case DAL.Enums.ItemEntityType.Mask: return new Mask(item);
                case DAL.Enums.ItemEntityType.Clothes: return new Clothes(item);
                case DAL.Enums.ItemEntityType.Water: return new Water(item);
                case DAL.Enums.ItemEntityType.Obiect: return new Object(item);
                //case DAL.Enums.ItemEntityType.WeaponClip:
                //case DAL.Enums.ItemEntityType.Food:
                default:
                    throw new NotSupportedException("");
            }
        }
    }
}
