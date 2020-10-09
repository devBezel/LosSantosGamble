using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Economy.Base.Jobs;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Entities.Core.Warehouse;
using LSG.GM.Entities.Job;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Jobs.Base.Courier
{
    public class CourierJob : JobEntity
    {
        JobClotheModel JobClothe = new JobClotheModel()
        {

        };


        public CourierJob(JobEntityModel jobEnityModel) : base(jobEnityModel)
        {
            
        }



        public List<WarehouseOrderModel> GetGroupWarehouseOrders()
        {
            List<WarehouseOrderModel> warehouseOrders = new List<WarehouseOrderModel>();
            foreach (WarehouseOrderEntity order in EntityHelper.GetAllWarehouseOrders())
            {
                if(!order.IsDelivered)
                {
                    warehouseOrders.Add(order.DbModel);
                }
            }

            return warehouseOrders;
        }

        public void Dispose(CharacterEntity worker)
        {
            if (worker.CurrentDeliveryOrder != null)
            {
                worker.AccountEntity.Player.RemoveDrawText($"WAREHOUSE_ORDER_DRAW_TEXT{worker.CurrentDeliveryOrder.DbModel.Id}");
                Task.Run(async () =>
                {
                    await worker.AccountEntity.Player.DeleteBlip($"WAREHOUSE_ORDER_BLIP{worker.CurrentDeliveryOrder.DbModel.Id}");
                });
                worker.AccountEntity.Player.CallNative("clearGpsMultiRoute");

                worker.CurrentDeliveryOrder.CurrentCourier = null;
                worker.CurrentDeliveryOrder.IsDelivered = false;

                worker.CurrentDeliveryOrder = null;
            }
        }
    }
}
