using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public static class ClothesFactory
    {
        public static void ChangeClothes(CharacterEntity sender, int componentID, int drawableId, int textureId)
        {
            switch (componentID)
            {
                case (int)ClothesType.Hat:
                    sender.DbModel.CharacterLook.HatId = drawableId;
                    sender.DbModel.CharacterLook.HatTexture = textureId;
                    break;
                case (int)ClothesType.Glasses:
                    sender.DbModel.CharacterLook.GlassesId = drawableId;
                    sender.DbModel.CharacterLook.GlassesTexture = textureId;
                    break;
                case (int)ClothesType.Legs:
                    sender.DbModel.CharacterLook.LegsId = drawableId;
                    sender.DbModel.CharacterLook.LegsTexture = textureId;
                    break;
                case (int)ClothesType.Shoes:
                    sender.DbModel.CharacterLook.ShoesId = drawableId;
                    sender.DbModel.CharacterLook.ShoesTexture = textureId;
                    break;
                case (int)ClothesType.Undershirt:
                    sender.DbModel.CharacterLook.UndershirtId = drawableId;
                    sender.DbModel.CharacterLook.UndershirtTexture = drawableId;
                    break;
                case (int)ClothesType.Top:
                    sender.DbModel.CharacterLook.TopId = drawableId;
                    sender.DbModel.CharacterLook.TopTexture = textureId;
                    break;
                default:
                    break;
            }
        }
    }
}
