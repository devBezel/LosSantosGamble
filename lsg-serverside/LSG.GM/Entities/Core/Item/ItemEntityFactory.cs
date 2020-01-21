using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;
using static LSG.GM.Entities.Base.Interfaces.IEntityFactory;

namespace LSG.GM.Entities.Core.Item
{
    public class ItemEntityFactory : IEntityFactory<ItemEntity, CharacterItem>
    {
        public ItemEntity Create(CharacterItem item)
        {
            switch (item.ItemEntityType)
            {
                case DAL.Enums.ItemEntityType.Weapon: return new Weapon(item);
                //case DAL.Enums.ItemEntityType.WeaponClip:
                //case DAL.Enums.ItemEntityType.Food:
                default:
                    throw new NotSupportedException("");
            }
        }
    }
}
