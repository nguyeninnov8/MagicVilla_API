using MagicVilla.Data;
using MagicVilla.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace MagicVilla.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> querry = _dbSet;
            if (filter != null)
            {
                querry = querry.Where(filter);
            }
            return await querry.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> querry = _dbSet;
            if (tracked == true)
            {
                querry = querry.AsNoTracking();
            }
            if (filter != null)
            {
                querry = querry.Where(filter);
            }
            return await querry.FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
