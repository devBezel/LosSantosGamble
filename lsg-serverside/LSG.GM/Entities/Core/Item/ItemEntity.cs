using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public class ItemEntity
    {
        public int Id => DbModel.Id;
        public string Name => DbModel.Name;

        protected CharacterItem DbModel { get; }
        protected ItemEntity(CharacterItem item)
        {
            DbModel = item;
        }

        public virtual void UseItem(CharacterEntity characterEntity)
        {
            //na logi
        }

        protected virtual void Save()
        {
            // Zrobić zapis
        }


        protected virtual void Delete()
        {
            // Zrobić usuwanie
        }
    }
}
