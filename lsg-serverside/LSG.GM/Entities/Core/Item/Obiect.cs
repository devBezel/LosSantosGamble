using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public class Obiect : ItemEntity
    {
        public double? ObiectId => DbModel.FirstParameter;

        public Obiect(ItemModel item) : base(item)
        {

        }

        public override void UseItem(CharacterEntity sender)
        {
            if(sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {
                sender.AccountEntity.Player.SendErrorNotify("Wystąpił bląd", "Aby odużyć ten przedmiot musisz go zabrać z ziemi");
                return;
            }

            sender.ItemsInUse.Add(this);
            sender.AccountEntity.Player.Emit("item:useObject", DbModel.Id, ObiectId);
        }

    }
}
