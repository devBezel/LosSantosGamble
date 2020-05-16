using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.WarehouseModels
{
    public class WarehouseModel
    {
        public int Id { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        public List<WarehouseItemModel> Items { get; set; }
        public List<WarehouseOrderModel> WarehouseOrders { get; set; }
    }
}
