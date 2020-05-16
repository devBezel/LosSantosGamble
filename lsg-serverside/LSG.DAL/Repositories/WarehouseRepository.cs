using LSG.DAL.Database;
using LSG.DAL.Database.Models.WarehouseModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class WarehouseRepository : GenericRepository, IWarehouseRepository
    {
        private readonly RoleplayContext _context;

        public WarehouseRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<WarehouseModel>> GetAll()
        {
            return await _context.Warehouses
                .Include(w => w.Items)
                .Include(o => o.WarehouseOrders)
                .ToListAsync();
        }

        public async Task<List<WarehouseOrderModel>> GetAllOrders()
        {
            return await _context.WarehouseOrders
                .Include(w => w.Warehouse)
                .ThenInclude(g => g.Group)
                .ToListAsync();
        }
    }
}
