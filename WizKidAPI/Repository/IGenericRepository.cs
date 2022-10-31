using System.Linq.Expressions;

namespace WizKidAPI.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> select = null);
        public Task<List<TEntity>> GetAllByFilters(Expression<Func<TEntity, bool>> expression, int returnCount);

    }
}
