using AltV.Net;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    public abstract class ItemEntity
    {
        public int Id => DbModel.Id;
        public string Name => DbModel.Name;
        public ItemEntityType ItemEntityType => DbModel.ItemEntityType;

        protected ItemModel DbModel { get; }
        protected ItemEntity(ItemModel item)
        {
            DbModel = item;
        }

        public virtual void Create(CharacterEntity characterEntity)
        {
            if(characterEntity != null)
            {
                characterEntity.DbModel.Items.Add(DbModel);
            }
            Save();
            // Zrobić zapis do bazy danych tego itemu
        }

        public virtual void UseItem(CharacterEntity characterEntity)
        {
            Alt.Log($"[ITEM] {characterEntity.DbModel.Name} {characterEntity.DbModel.Surname} użył item ID: {Id} nazwa: {Name}");
        }

        public virtual void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.ItemRepository.Update(DbModel);
            }

            Alt.Log($"[ITEM-ENTITY]: Zapisałem item: [{DbModel.Id} | {DbModel.Name}]");
        }


        protected virtual void Delete()
        {
            // Zrobić usuwanie
        }
    }
}
