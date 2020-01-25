using LSG.DAL.Database.Models.CharacterModels;
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
                case 0:
                    sender.DbModel.CharacterLook.HatId = drawableId;
                    sender.DbModel.CharacterLook.HatTexture = textureId;
                    break;
                case 1:
                    sender.DbModel.CharacterLook.GlassesId = drawableId;
                    sender.DbModel.CharacterLook.GlassesTexture = textureId;
                    break;
                case 4:
                    sender.DbModel.CharacterLook.LegsId = drawableId;
                    sender.DbModel.CharacterLook.LegsTexture = textureId;
                    break;
                case 6:
                    sender.DbModel.CharacterLook.ShoesId = drawableId;
                    sender.DbModel.CharacterLook.ShoesTexture = textureId;
                    break;
                case 8:
                    sender.DbModel.CharacterLook.UndershirtId = drawableId;
                    sender.DbModel.CharacterLook.UndershirtTexture = drawableId;
                    break;
                case 11:
                    sender.DbModel.CharacterLook.TopId = drawableId;
                    sender.DbModel.CharacterLook.TopTexture = textureId;
                    break;
                default:
                    break;
            }
        }
    }
}
