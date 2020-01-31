using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Item.Scripts;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core
{
    public class CharacterEntity
    {
        public AccountEntity AccountEntity { get; private set; }
        public Character DbModel { get; set; }
        internal List<ItemEntity> ItemsInUse { get; set; } = new List<ItemEntity>();
        public bool HasBw { get; set; } = false;


        public string FormatName => $"{DbModel.Name} {DbModel.Surname}";


        public CharacterEntity(AccountEntity accountEntity, Character dbModel)
        {
            AccountEntity = accountEntity;
            DbModel = dbModel;
        }

        public async Task Spawn() => await AltAsync.Do(() =>
        {
            AccountEntity.Player.Spawn(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ));
            AccountEntity.Player.SetHealthAsync((ushort)DbModel.Health);
            AccountEntity.Player.SetModelAsync(0x705E61F2);
            AccountEntity.Player.SetNameAsync(DbModel.Name);
            UpdateName(FormatName);


            SetCharacterDataToClient();

            if (DbModel.Gender)
            {
                AccountEntity.Player.SetModelAsync(0x9C9EFFD8);
            }

            if (AccountEntity.HasPremium)
                AccountEntity.Player.SendChatMessage("Dziękujemy za wspieranie naszego projektu " + AccountEntity.DbModel.Username + "! Do końca twojego {D1BA0f} premium {ffffff} pozostało " +
                        Calculation.CalculateTheNumberOfDays(AccountEntity.DbModel.AccountPremium.EndTime, DateTime.Now) + " dni");

            if (DbModel.CharacterLook == null)
                AccountEntity.Player.Dimension = AccountEntity.DbModel.Id;


            List<ItemModel> activeItems = DbModel.Items.Where(item => item.ItemInUse).ToList();
            foreach (ItemModel item in activeItems)
            {
                ItemEntity itemEntity = InventoryScript.ItemFactory.Create(item);
                itemEntity.UseItem(this);
            }

            AccountEntity.Player.EmitAsync("character:wearClothes", DbModel.CharacterLook);
        });

        public Position CharacterPosition
        {
            get
            {
                return AccountEntity.Player.Position;
            }
            set
            {
                DbModel.PosX = AccountEntity.Player.Position.X;
                DbModel.PosY = AccountEntity.Player.Position.Y;
                DbModel.PosZ = AccountEntity.Player.Position.Z;
            }
        }


        public void Save()
        {

            if (AccountEntity != null)
            {
                CharacterPosition = AccountEntity.Player.Position;
            }

            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.CharacterRepository.Update(DbModel);
            }
            SetCharacterDataToClient();
        }

        public void UpdateName(string newName)
        {
            AccountEntity.Player.SetSyncedMetaData("character:name", newName);
        }

        public void SetCharacterDataToClient()
        {
            CharacterForListDto characterDto = Singleton.AutoMapper().Map<CharacterForListDto>(DbModel);
            AccountEntity.Player.SetSyncedMetaData("character:dataCharacter", characterDto);
        }

        public void AddMoney(int amount)
        {
            DbModel.Money += amount;
        }

        public void RemoveMoney(int amount)
        {
            DbModel.Money -= amount;
        }

        public bool HasEnoughMoney(int amount)
        {
            return (DbModel.Money >= amount) ? true : false;
        }

        public bool HasActiveItem(ItemEntityType itemType)
        {
            if (!ItemsInUse.Any(item => item.ItemEntityType == itemType))
                return false;
            else
                return true;
        }

    }
}
