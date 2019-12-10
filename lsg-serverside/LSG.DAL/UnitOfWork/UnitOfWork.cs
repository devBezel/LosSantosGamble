using LSG.DAL.Database;
using LSG.DAL.Database.Models;
using LSG.DAL.Repositories;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoleplayContext _context;

        public UnitOfWork(RoleplayContext context)
        {
            _context = context;
            CharacterRepository = new CharacterRepository(_context);
        }

        public ICharacterRepository CharacterRepository { get; set; }

        public void Dispose()
        {
            _context.SaveChanges();
        }
    }
}
