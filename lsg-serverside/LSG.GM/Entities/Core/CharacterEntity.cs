﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.UnitOfWork;
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
        public bool HasBw { get; set; } = false;

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
            SendCharacterDataToClient();

            if (DbModel.Gender)
            {
                AccountEntity.Player.SetModelAsync(0x9C9EFFD8);
            }

            if (AccountEntity.HasPremium)
                AccountEntity.Player.SendChatMessage("Dziękujemy za wspieranie naszego projektu " + AccountEntity.DbModel.Username + "! Do końca twojego {D1BA0f} premium {ffffff} pozostało " +
                        Calculation.CalculateTheNumberOfDays(AccountEntity.DbModel.AccountPremium.EndTime, DateTime.Now) + " dni");

            AccountEntity.Player.EmitAsync("character:wearClothes", DbModel.CharacterLook);
        });

        public void Save()
        {

            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.CharacterRepository.Update(DbModel);
            }
            SendCharacterDataToClient();
        }

        public void SendCharacterDataToClient()
        {
            CharacterForListDto characterDto = Singleton.AutoMapper().Map<CharacterForListDto>(DbModel);
            AccountEntity.Player.Emit("character:sendDataCharacter", characterDto);
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

    }
}
