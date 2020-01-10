using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Repositories;
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
    public class AccountEntity : IDisposable
    {
        public Account DbModel { get; set; }
        public CharacterEntity characterEntity;
        public IPlayer Player { get; set; }

        public AccountEntity(Account dbModel, IPlayer player)
        {
            DbModel = dbModel;
            Player = player;
        }

        public async void Login(Character character)
        {
            Player.SetData("account:data", this);
            Player.SetData("account:id", Calculation.GenerateFreeIdentifier());
            SendAccountDataToClient();

            characterEntity = new CharacterEntity(this, character);
            EntityHelper.Add(this);

            AltAsync.Log($"{DbModel.Rank}");
            await characterEntity.Spawn();
        }

        public int ServerID
        {
            get
            {
                Player.GetData("account:id", out int serverId);
                return serverId;
            }
        }

        public bool HasPremium
        {
            get
            {
                if (DbModel.AccountPremium == null || (DbModel.AccountPremium.EndTime <= DateTime.Now))
                {
                    Player.Emit("account:hasPremium", false);

                    return false;
                }

                Player.Emit("account:hasPremium", true);
                return true;

            }
        }

        public bool OnAdminDuty
        {
            get
            {
                Player.GetData("admin:duty", out bool onDuty);

                if (!onDuty)
                {
                    return false;
                }

                return true;
            }
        }

        public void ChangeAdminDutyState()
        {
            if (OnAdminDuty)
            {
                Player.SetData("admin:duty", false);
                Player.Emit("admin:setDuty", false);

                Player.SendSuccessNotify("Wykonano pomyślnie!", "Zszedłeś ze służby admina poprawnie!");
                return;
            }

            Player.SetData("admin:duty", true);
            Player.Emit("admin:setDuty", true);

            Player.SendSuccessNotify("Wykonano pomyślnie!", "Wszedłeś na służbę admina poprawnie!");
        }


        public bool HasRank(int rank)
        {
            return DbModel.Rank >= rank ? true : false;
        }

        public void SendAccountDataToClient()
        {
            AccountForCharacterDto accountDto = Singleton.AutoMapper().Map<AccountForCharacterDto>(DbModel);
            Player.Emit("account:sendDataAccount", accountDto, ServerID);
        }

        public void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.AccountRepository.Update(DbModel);
            }
        }

        public void Dispose()
        {
            EntityHelper.Remove(this);
            Save();
        }
    }
}

