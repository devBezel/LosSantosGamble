using LSG.DAL.Database;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class BuildingRepository : GenericRepository, IBuildingRepository
    {
        private readonly RoleplayContext _context;
        public BuildingRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<BuildingModel>> GetAll()
        {
            return await _context.Buildings.Include(items => items.ItemsInBuilding).ToListAsync();
        }
    }
}
