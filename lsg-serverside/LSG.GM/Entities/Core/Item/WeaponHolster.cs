using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    internal class WeaponHolster : ItemEntity
    {
        public int GunHolsterObject => (int)DbModel.FirstParameter.Value;

        public WeaponHolster(ItemModel item) : base(item) { }

        public override void UseItem(CharacterEntity sender)
        {
            if(sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {
                DbModel.ItemInUse = false;

                Save();

                sender.AccountEntity.Player.Emit("item:weaponHolsterHide", DbModel.Character.CharacterLook.UndershirtId, DbModel.Character.CharacterLook.UndershirtTexture);
                sender.ItemsInUse.Remove(this);
            }
            else
            {
                DbModel.ItemInUse = true;
                sender.AccountEntity.Player.Emit("item:weaponHolsterTakeOut", GunHolsterObject);
                sender.ItemsInUse.Add(this);
            }
        }

    }
}
