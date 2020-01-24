using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Constant;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    internal class Mask : ItemEntity
    {
        public int MaskObject => (int)DbModel.FirstParameter.Value;
        public int UseCount => (int)DbModel.SecondParameter.Value;

        public Mask(ItemModel item): base(item)
        {

        }

        public override void UseItem(CharacterEntity sender)
        {
            string encryptedName = $"Nieznajomy {sender.FormatName.GetHashCode().ToString().Substring(1, 6)}";

            if(sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {
                sender.UpdateName(sender.FormatName);
                sender.ItemsInUse.Remove(this);

                DbModel.ItemInUse = false;
                Save();

                sender.AccountEntity.Player.Emit("item:maskHide");
            }
            else if(sender.ItemsInUse.All(item => !(item is Mask)))
            {
                if(UseCount == 0)
                {
                    sender.AccountEntity.Player.SendNativeNotify(null, NotificationNativeType.Error, 1, "Ta maska się przetarła", "~g~Ekwipunek", "Ta maska uległa przetarciu po ostatniej akcji", 1);
                    return;
                }

                sender.UpdateName(encryptedName);
                sender.ItemsInUse.Add(this);

                DbModel.SecondParameter--;
                DbModel.ItemInUse = true;
                Save();


                sender.AccountEntity.Player.Emit("item:maskEquip", MaskObject);

            }

        }
    }
}
