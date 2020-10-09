using LSG.DAL.Database.Models.WarehouseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IWarehouseRepository : IGenericRepository
    {
        Task<List<WarehouseModel>> GetAll();
        Task<List<WarehouseOrderModel>> GetAllOrders();
    }
}
