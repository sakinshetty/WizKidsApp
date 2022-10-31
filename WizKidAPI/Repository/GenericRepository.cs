using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using WizKidAPI.Database;

namespace WizKidAPI.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public WordDBContext _dbContext;
        public DbSet<TEntity> dbSet;
        public GenericRepository(WordDBContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> select = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (select != null)
            {
                query = query.Select(select);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<List<TEntity>> GetAllByFilters(Expression<Func<TEntity, bool>> expression, int returnCount)
        {

            return await _dbContext.Set<TEntity>().Where(expression).Take(returnCount).ToListAsync();
        }
    }
}
