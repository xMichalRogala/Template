using System.Linq.Expressions;

namespace Template.Application.Core.Abstractions.Data
{
    public interface IQueryableGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllAsQueryable();

        IQueryable<TEntity> WhereAsQueryable(Expression<Func<TEntity, bool>> predicate);
    }
}
