using AltV.Net.Async;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AltV.Net;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.GM.Entities.Core.Warehouse;

namespace LSG.GM.Entities.Core.Group
{
    public abstract class GroupEntity
    {
        public GroupModel DbModel { get; set; }
        public long Id => DbModel.Id;

        public List<AccountEntity> PlayersOnDuty = new List<AccountEntity>();


        protected GroupEntity(GroupModel groupModel)
        {
            DbModel = groupModel;
            EntityHelper.Add(this);
        }

        public static GroupEntity Create(string name, string tag, GroupType type)
        {
            GroupModel groupModel = new GroupModel()
            {
                Workers = new List<GroupWorkerModel>(),
                Name = name, 
                Tag = tag,
                GroupType = type,
            };

            RoleplayContext context = Singleton.GetDatabaseInstance();
            using(UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                unitOfWork.GroupRepository.Add(groupModel);
            }

            GroupEntityFactory entityFactory = new GroupEntityFactory();
            return entityFactory.Create(groupModel);
        }

        public IEnumerable<GroupWorkerModel> GetWorkers()
        {
            return DbModel.Workers.Where(w => w.Character != null);
        }

        public bool IsGroupOwner(GroupWorkerModel worker)
        {
            return DbModel.LeaderId == worker.CharacterId ? true : false;
        }

        public bool CanPlayerManageWorkers(GroupWorkerModel worker)
        {
            return worker.Rights.HasFlag(GroupRights.Recruitment) || IsGroupOwner(worker) ? true : false;
        }

        public bool CanPlayerVehicle(GroupWorkerModel worker)
        {
            return worker.Rights.HasFlag(GroupRights.Vehicle) || IsGroupOwner(worker) ? true : false;
        }

        //public bool CanPlayerOpenGroupPanel(GroupWorkerModel worker)
        //{
        //    return worker?.Rights.HasFlag(GroupRights.Panel) ?? false;
        //}

        public void AddMoney(int money)
        {
            DbModel.Money += money;
            Save();
        }

        public void RemoveMoney(int money)
        {
            DbModel.Money -= money;
            Save();
        }

        public bool HasMoney(int money)
        {
            return (DbModel.Money >= money) ? true : false;
        }

        public bool ContainsWorker(AccountEntity accountEntity)
        {
            return DbModel.Workers.Any(c => c.CharacterId == accountEntity.characterEntity.DbModel.Id);
        }

        public void AddWorker(AccountEntity accountEntity)
        {
            GroupWorkerModel groupWorker = new GroupWorkerModel()
            {
                Group = DbModel,
                Character = accountEntity.characterEntity.DbModel,
                Salary = 0,
                Rights = GroupRights.None,
                DutyMinutes = 0,
            };

            DbModel.Workers.Add(groupWorker);
            Save();
        }


        public void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using(UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.GroupRepository.Update(DbModel);
            }
        }

        public static async Task LoadGroupsAsync(UnitOfWork unitOfWork)
        {
            GroupEntityFactory factory = new GroupEntityFactory();
            foreach (GroupModel group in await unitOfWork.GroupRepository.GetAll())
            {
                GroupEntity groupEntity = factory.Create(group);

                foreach (WarehouseModel warehouse in await unitOfWork.WarehouseRepository.GetAll())
                {
                    if(groupEntity.DbModel.Id == warehouse.GroupId)
                    {
                        WarehouseEntity warehouseEntity = new WarehouseEntity(groupEntity, warehouse);
                        warehouseEntity.Spawn();
                    }
                }
            }
        }
    }
}
