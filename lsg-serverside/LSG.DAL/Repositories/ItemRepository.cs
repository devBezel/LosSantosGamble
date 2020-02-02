using LSG.DAL.Database;
using System;
using System.Collections.Generic;
using System.Text;
using LSG.DAL.Repositories.IRepository;

namespace LSG.DAL.Repositories
{
    public class ItemRepository : GenericRepository, IItemRepository
    {
        private readonly RoleplayContext _context;
        public ItemRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }
    }
}
