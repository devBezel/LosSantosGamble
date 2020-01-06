using LSG.DAL.Database;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Repositories
{
    public class VehicleRepository : GenericRepository, IVehicleRepository
    {
        private readonly RoleplayContext _context;

        public VehicleRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }


    }
}
