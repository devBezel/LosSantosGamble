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
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Item.Scripts;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BaseServer = LSG.GM.Base;

namespace LSG.GM.Entities.Core
{
    public class CharacterEntity
    {
        public AccountEntity AccountEntity { get; private set; }
        public Character DbModel { get; set; }
        public GroupEntity OnDutyGroup { get; set; }
        internal List<ItemEntity> ItemsInUse { get; set; } = new List<ItemEntity>();

        public Timer SpentTimer { get; set; }
        public bool IsAfk { get; set; }
        public bool HasBw { get; set; } = false;

        public int RespawnVehicleCount { get; set; }


        public bool IsHandcuffed { get; set; } = false;
        public bool IsDragged { get; set; } = false;


        public string FormatName => $"{DbModel.Name} {DbModel.Surname}";


        public CharacterEntity(AccountEntity accountEntity, Character dbModel)
        {
            AccountEntity = accountEntity;
            DbModel = dbModel;
        }

        public async Task Spawn()
        {
            await AltAsync.Do(() =>
            {
                AccountEntity.Player.Spawn(new Position(DbModel.PosX, DbModel.PosY, DbModel.PosZ));
                AccountEntity.Player.SetDateTime(DateTime.Now);
            });

            await AccountEntity.Player.SetHealthAsync((ushort)DbModel.Health);
            await AccountEntity.Player.SetModelAsync(0x705E61F2);
            //AccountEntity.Player.SetNameAsync(DbModel.Name);
            await AccountEntity.Player.SetSyncedMetaDataAsync("character:hunger", DbModel.Hunger);
            await AccountEntity.Player.SetSyncedMetaDataAsync("character:thirsty", DbModel.Thirsty);
            AccountEntity.Player.SetSyncedMetaData("character:money", DbModel.Money);
            Dimension = DbModel.Dimension;
            DbModel.Online = true;
            UpdateName(FormatName);


            SetCharacterDataToClient();

            if (DbModel.Gender)
            {
               await AccountEntity.Player.SetModelAsync(0x9C9EFFD8);
            }

            AccountEntity.Player.SendChatMessageInfo($"Witamy na Los Santos Gamble, wersja {BaseServer.FormatServerVersion}");
            AccountEntity.Player.SendChatMessageInfo($"{DbModel.Account.Username}, ostatnio grałeś u nas {DbModel.RecentlyPlayed} na tej postaci. Dziękujemy i życzymy miłej gry");

            if (AccountEntity.HasPremium)
                AccountEntity.Player.SendChatMessage("Dziękujemy za wspieranie naszego projektu " + AccountEntity.DbModel.Username + "! Do końca twojego {D1BA0f} premium {ffffff} pozostało " +
                        Calculation.CalculateTheNumberOfDays(AccountEntity.DbModel.AccountPremium.EndTime, DateTime.Now) + " dni");

            if (DbModel.CharacterLook == null)
                AccountEntity.Player.Dimension = AccountEntity.DbModel.Id;

            DbModel.RecentlyPlayed = DateTime.Now;

            List<ItemModel> activeItems = DbModel.Items.Where(item => item.ItemInUse).ToList();
            foreach (ItemModel item in activeItems)
            {
                ItemEntity itemEntity = InventoryScript.ItemFactory.Create(item);
                itemEntity.UseItem(this);
            }

            SpentTimer = new Timer(60000);
            SpentTimer.Start();
            SpentTimer.Elapsed += (o, args) =>
            {
                DbModel.TimeSpent += 1;
            };

            

            await AccountEntity.Player.EmitAsync("character:wearClothes", DbModel.CharacterLook);
        }

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

            Alt.Log($"[CHARACTER-ENTITY]: Zapisałem postać: [{DbModel.Id} | {DbModel.Name} {DbModel.Surname}]");
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

        public void AddMoney(int amount, bool bank)
        {
            // Do zrobienia
            if(bank)
            {
                DbModel.Bank += amount;
            } else
            {
                DbModel.Money += amount;
            }
            AccountEntity.Player.SetSyncedMetaData("character:money", DbModel.Money);

            //SetCharacterDataToClient();
        }

        public void RemoveMoney(int amount, bool bank)
        {
            if(bank)
            {
                DbModel.Bank -= amount;

            }
            else
            {
                DbModel.Money -= amount;
            }
            AccountEntity.Player.SetSyncedMetaData("character:money", DbModel.Money);
        }

        public bool HasEnoughMoney(int amount, bool bank)
        {
            if (bank)
                return (DbModel.Bank >= amount) ? true : false;

            return (DbModel.Money >= amount) ? true : false;
        }

        public bool HasActiveItem(ItemEntityType itemType)
        {
            if (!ItemsInUse.Any(item => item.ItemEntityType == itemType))
                return false;
            else
                return true;
        }

        public void UnBw()
        {
            Hunger = 100;
            Thirsty = 100;

            AccountEntity.Player.Emit("bw:revive");
        }

        public float Hunger
        {
            get
            {
                return DbModel.Hunger;
            }

            set
            {
                DbModel.Hunger = value;
                AccountEntity.Player.SetSyncedMetaData("character:hunger", DbModel.Hunger);
            }
        }

        public float Thirsty
        {
            get
            {
                return DbModel.Thirsty;
            }

            set
            {
                DbModel.Thirsty = value;
                AccountEntity.Player.SetSyncedMetaData("character:thirsty", DbModel.Thirsty);
            }
        }

        public int Dimension
        {
            get
            {
                return AccountEntity.Player.Dimension;
            }
            set
            {
                AccountEntity.Player.SetSyncedMetaData("player:dimension", value);
                AccountEntity.Player.Dimension = value;
                DbModel.Dimension = value;
            }
        }

    }
}
