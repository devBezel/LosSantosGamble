using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public abstract class ItemEntity
    {
        public int Id => DbModel.Id;
        public string Name => DbModel.Name;

        protected ItemModel DbModel { get; }
        protected ItemEntity(ItemModel item)
        {
            DbModel = item;
        }

        public virtual void UseItem(CharacterEntity characterEntity)
        {
            Alt.Log($"[ITEM] {characterEntity.DbModel.Name} {characterEntity.DbModel.Surname} użył item ID: {Id} nazwa: {Name}");
        }

        protected virtual void Save()
        {
            Alt.Log("Zapisuje item");
            // Zrobić zapis
        }


        protected virtual void Delete()
        {
            // Zrobić usuwanie
        }
    }
}
