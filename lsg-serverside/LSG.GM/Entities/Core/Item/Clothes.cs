using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Constant;
using LSG.GM.Extensions;
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

                switch (ComponentId)
                {
                    case (int)ClothesType.Legs: ClothesFactory.ChangeClothes(sender, ComponentId, 21, 0);
                        break;
                    case (int)ClothesType.Shoes: ClothesFactory.ChangeClothes(sender, ComponentId, 34, 0);
                        break;
                    case (int)ClothesType.Undershirt:
                        ClothesFactory.ChangeClothes(sender, ComponentId, 15, 0);
                        ClothesFactory.ChangeClothes(sender, (int)ClothesType.Torso, 15, 0);
                        break;
                    case (int)ClothesType.Top:
                        ClothesFactory.ChangeClothes(sender, ComponentId, 15, 0);
                        ClothesFactory.ChangeClothes(sender, (int)ClothesType.Torso, 15, 0);
                        break;
                    default:
                        break;
                }
                sender.AccountEntity.Player.Emit("item:takeOffClothes", ComponentId);
            } else
            {
                Alt.Log("componentID: " + ComponentId);
                // Zapobiega to niewidzialnej klatce piersiowej, jak się zrobi eup z kaburą to raczej będzie to już niepotrzebne
                if(ComponentId == (int)ClothesType.Undershirt && sender.ItemsInUse.Any(item => item.ItemEntityType == ItemEntityType.WeaponHolster))
                {
                    sender.AccountEntity.Player.SendNativeNotify(null, NotificationNativeType.Error, 1, "Masz założoną kaburę", "~g~ Ekwipunek", "Nie możesz założyć podkoszulka podczas gdy nosisz na sobie kaburę, zdejmij kaburę");
                    return;
                }
                    

                sender.ItemsInUse.Add(this);
                DbModel.ItemInUse = true;

                ClothesFactory.ChangeClothes(sender, ComponentId, DrawableId, TextureId);

                sender.AccountEntity.Player.Emit("item:equipClothes", ComponentId, DrawableId, TextureId, sender.DbModel.CharacterLook.TorsoId, sender.DbModel.CharacterLook.TorsoTexture);
            }
        }
    }
}
