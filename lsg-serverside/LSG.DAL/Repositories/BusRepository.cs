using LSG.DAL.Database;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Repositories
{
    public class BusRepository : GenericRepository, IBusRepository
    {
        private readonly RoleplayContext _context;

        public BusRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }
    }
}
