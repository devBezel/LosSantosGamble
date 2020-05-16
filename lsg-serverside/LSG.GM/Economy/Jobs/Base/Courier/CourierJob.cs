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
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Jobs.Base.Courier
{
    public class CourierJob : JobEntity
    {

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
    }
}
