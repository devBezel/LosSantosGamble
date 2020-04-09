using LSG.DAL.Database;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class AtmRepository : GenericRepository, IAtmRepository
    {
        private readonly RoleplayContext _context;

        public AtmRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Atm>> GetAll()
        {
            List<Atm> atms = await _context.Atms.ToListAsync();

            return atms;
        }
    }
}
