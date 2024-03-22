using System.Linq.Expressions;

namespace Template.Application.Core.Abstractions.Data
{
    public interface IConcreteGenericRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync(CancellationToken ct);

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    }
}
