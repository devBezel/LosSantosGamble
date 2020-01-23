using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Constant;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Item
{
    internal class Weapon : ItemEntity
    {
        public WeaponModel WeaponHash => (WeaponModel)DbModel.FirstParameter.Value;
        public double Ammo => DbModel.SecondParameter.Value;

        public Weapon(ItemModel item) : base (item)
        {

        }

        public override void UseItem(CharacterEntity sender)
        {            
            if (Ammo <= 0)
            {
                sender.AccountEntity.Player.SendNativeNotify(null, NotificationNativeType.Error, 0, "Ta broń nie posiada amunicji", "Broń", "Załaduj magazynek, aby móc użyć tej broni", 1);
                return;
            }

            if (sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {                
                //TODO: Dokończyć amunicje
                DbModel.SecondParameter = 100;
                Save();

                sender.AccountEntity.Player.RemoveAllWeapons();
                sender.ItemsInUse.Remove(this);
                sender.AccountEntity.Player.Emit("item:weaponHide", (uint)WeaponHash);
            }
            else
            {
                sender.ItemsInUse.Add(this);
                sender.AccountEntity.Player.GiveWeapon((uint)WeaponHash, (int)Ammo, true);
                sender.AccountEntity.Player.Emit("item:weaponTakeOut", (uint)WeaponHash);
                
            }
        }
    }
}