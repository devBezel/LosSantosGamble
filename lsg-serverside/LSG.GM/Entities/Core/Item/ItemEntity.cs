using AltV.Net;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Extensions;
using LSG.GM.Helpers.Models;
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
            if (characterEntity != null)
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


        protected virtual void Remove()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.ItemRepository.Delete(DbModel);
            }
        }

        // TODO: przenieść to do osobnego skryptu z ofertami
        //public virtual void Offer(CharacterEntity sender, CharacterEntity getter, int cost)
        //{
        //    if (DbModel.ItemInUse) return;

        //    if(!getter.HasEnoughMoney(cost, false))
        //    {
        //        getter.AccountEntity.Player.SendErrorNotify("Brak wystarczającej ilości gotówki", "Nie posiadasz przy sobie tyle gotówki");
        //        return;
        //    }

        //    if(cost != 0)
        //    {
        //        sender.AddMoney(cost, false);
        //        getter.RemoveMoney(cost, false);
        //    }

        //    DbModel.CharacterId = getter.DbModel.Id;

        //    sender.DbModel.Items.Remove(DbModel);
        //    getter.DbModel.Items.Add(DbModel);

        //    sender.AccountEntity.Player.SendSuccessNotify("Gracz zaakceptował ofertę", "Gracz pomyślnie zaakceptował twoją ofertę");
        //    getter.AccountEntity.Player.SendSuccessNotify("Zaakceptowałeś ofertę", "Oferta gracza została zaakceptowana pomyślnie");

        //}

        public virtual void Confiscate(CharacterEntity robber, CharacterEntity robbed)
        {
            Alt.Log("ItemInUse: " + DbModel.ItemInUse);
            if(DbModel.ItemInUse)
            {
                // Odużywa item jeśli jest użyty
                UseItem(robbed);
            }

            DbModel.Character = robber.DbModel;
            Save();
            //robbed.DbModel.Items.Remove(DbModel);
            //robber.DbModel.Items.Add(DbModel);

        }

        public virtual void PickUp(CharacterEntity characterEntity)
        {
            if (DbModel.CharacterId != null) return;

            
        }

        public virtual void Drop(CharacterEntity characterEntity)
        {
            if (DbModel.ItemInUse) return;

            if (DbModel.CharacterId == null) return;

            DbModel.CharacterId = null;
            ItemInWorldModel itemInWorld = new ItemInWorldModel()
            {
                ItemEntity = this,
                Position = characterEntity.AccountEntity.Player.Position,
                Dimension = characterEntity.AccountEntity.Player.Dimension
            };

            EntityHelper.Add(itemInWorld);

            // Wykonanie animacji + stworzenie obiektu + dxText
            
        }
    }
}
