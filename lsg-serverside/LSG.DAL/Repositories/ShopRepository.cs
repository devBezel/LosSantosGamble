using LSG.DAL.Database;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class ShopRepository : GenericRepository, IShopRepository
    {
        private readonly RoleplayContext _context;
        public ShopRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ShopModel>> GetAll()
        {
            List<ShopModel> shops = await _context.Shops.Include(a => a.ShopAssortments).ToListAsync();

            return shops;
        }
    }
}
