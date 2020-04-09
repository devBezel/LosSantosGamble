using LSG.DAL.Database;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class BusRepository : GenericRepository, IBusRepository
    {
        private readonly RoleplayContext _context;

        public BusRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BusStop>> GetAll()
        {
            List<BusStop> busStops = await _context.BusStops.Include(s => s.BusStopStations).ToListAsync();

            return busStops;
        }
    }
}
