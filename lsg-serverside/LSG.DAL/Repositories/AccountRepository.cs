using LSG.DAL.Database;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Repositories
{
    public class AccountRepository : GenericRepository, IAccountRepository
    {
        private readonly RoleplayContext _context;

        public AccountRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }
    }
}
