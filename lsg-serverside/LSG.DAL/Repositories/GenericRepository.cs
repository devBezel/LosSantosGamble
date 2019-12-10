using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.DAL.Database;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LSG.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RoleplayContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(RoleplayContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task <IEnumerable<T>> GetAllAsync()
        {
            var entity = await _dbSet.ToListAsync();
            return entity;
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
