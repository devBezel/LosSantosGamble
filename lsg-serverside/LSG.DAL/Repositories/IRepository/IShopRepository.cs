using LSG.DAL.Database.Models.ShopModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IShopRepository : IGenericRepository
    {
        Task<List<ShopModel>> GetAll();
    }
}
