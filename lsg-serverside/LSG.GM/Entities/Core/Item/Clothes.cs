using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    internal class Clothes : ItemEntity
    {
        public int ComponentId => (int)DbModel.FirstParameter.Value;
        public int DrawableId => (int)DbModel.SecondParameter.Value;
        public int TextureId => (int)DbModel.ThirdParameter.Value;

        public Clothes(ItemModel item) : base(item)
        {

        }

        public override void UseItem(CharacterEntity sender)
        {
            if(sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {
                sender.ItemsInUse.Remove(this);
                DbModel.ItemInUse = false;

                ClothesFactory.ChangeClothes(sender, ComponentId, 0, 0);
                sender.AccountEntity.Player.Emit("item:takeOffClothes", ComponentId);
            } else
            {
                sender.ItemsInUse.Add(this);
                DbModel.ItemInUse = true;

                ClothesFactory.ChangeClothes(sender, ComponentId, DrawableId, TextureId);

                sender.AccountEntity.Player.Emit("item:equipClothes", ComponentId, DrawableId, TextureId, sender.DbModel.CharacterLook.TorsoId, sender.DbModel.CharacterLook.TorsoTexture);
            }
        }
    }
}
