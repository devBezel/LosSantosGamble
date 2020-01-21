using AltV.Net;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Constant;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public class Weapon : ItemEntity
    {
        public WeaponModel WeaponHash => (WeaponModel)DbModel.FirstParameter.Value;
        public double Ammo => DbModel.SecondParameter.Value;

        public Weapon(CharacterItem item) : base (item)
        {

        }

        public override void UseItem(CharacterEntity sender)
        {
            Alt.Log("Wykonuje isę przy uzyciu broni 0");
            if (Ammo <= 0)
            {
                sender.AccountEntity.Player.SendNativeNotify(null, NotificationNativeType.Error, 0, "Ta broń nie posiada amunicji", "Broń", "Załaduj magazynek, aby móc użyć tej broni", 1);
                return;
            }

            if(sender.ItemInUse.Any(item => ReferenceEquals(item, this)))
            {
                Alt.Log("Wykonuje isę przy uzyciu broni 1");
                DbModel.SecondParameter = sender.AccountEntity.Player.GetCurrentWeaponTintIndex();
                Save();

                sender.AccountEntity.Player.RemoveWeapon((uint)WeaponHash);
                sender.ItemInUse.Remove(this);

            } else
            {
                Alt.Log("Wykonuje isę przy uzyciu broni 3");
                Alt.Log("WeaponHASH: " + (uint)WeaponHash);
                Alt.Log("Imie i nazwisko: " + sender.DbModel.Name + " " + sender.DbModel.Surname);
                sender.AccountEntity.Player.GiveWeapon((uint)WeaponHash, (int)Ammo, true);
                sender.ItemInUse.Add(this);

                //Zrobić zapis jeśli wyjdzie (czyli pobrać aktualne wartości broni i updatować do dbmodelu postaci (listy itemów))
            }
        }
    }
}
