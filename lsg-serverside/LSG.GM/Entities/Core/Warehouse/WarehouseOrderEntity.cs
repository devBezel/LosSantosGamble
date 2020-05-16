using LSG.DAL.Database;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.UnitOfWork;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Warehouse
{
    public class WarehouseOrderEntity
    {
        public WarehouseOrderModel DbModel { get; set; }
        public CharacterEntity CurrentCourier { get; set; }
        public bool IsDelivered { get; set; }

        public WarehouseOrderEntity(WarehouseOrderModel dbModel)
        {
            DbModel = dbModel;
        }

        public void Create()
        {
            EntityHelper.Add(this);

            Save();
        }

        public void Spawn()
        {
            EntityHelper.Add(this);
        }

        public void StartDeliviery(CharacterEntity courier)
        {
            CurrentCourier = courier;
            IsDelivered = true;

            courier.CurrentDeliveryOrder = this;
        }

        public void Delivier(CharacterEntity courier)
        {
            WarehouseItemModel itemDelivier = new WarehouseItemModel()
            {
                Name = DbModel.Name,
                Count = DbModel.Count,
                Cost = 0,
                FirstParameter = DbModel.FirstParameter,
                SecondParameter = DbModel.SecondParameter,
                ThirdParameter = DbModel.ThirdParameter,
                FourthParameter = DbModel.FourthParameter,
                ItemEntityType = DbModel.ItemEntityType,
                WarehouseId = DbModel.WarehouseId,
                Warehouse = DbModel.Warehouse
            };

            courier.CurrentDeliveryOrder = null;

            DbModel.Warehouse.Items.Add(itemDelivier);

            Save();

            Dispose();
        }

        public void Save()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.WarehouseRepository.Update(DbModel);
            }
        }

        public void Dispose()
        {
            EntityHelper.Remove(this);
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                unitOfWork.WarehouseRepository.Delete(DbModel);
            }
        }

        public static async Task LoadWarehouseOrdersAsync()
        {
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
            {
                foreach (WarehouseOrderModel order in await unitOfWork.WarehouseRepository.GetAllOrders())
                {
                    WarehouseOrderEntity orderEntity = new WarehouseOrderEntity(order);
                    orderEntity.Spawn();
                }
            }
        }
    }
}
