using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LSG.DAL.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly RoleplayContext _context;
        public GenericRepository(RoleplayContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
