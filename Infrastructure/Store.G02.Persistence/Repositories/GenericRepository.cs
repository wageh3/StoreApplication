using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repositories
{
    public class GenericRepository<TKey, TEntity> (StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        //Static Querrying
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            return changeTracker ?
                await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
        //Dynamic Querrying with Specifications Pattern
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey, TEntity> spec, bool changeTracker = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TKey, TEntity> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TKey, TEntity> spec)
        {
            return SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec);
        }
    }
}
