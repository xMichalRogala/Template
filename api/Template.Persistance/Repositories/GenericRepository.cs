using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Template.Application.Core.Abstractions.Data;
using Template.Domain.Entities.Abstract;

namespace Template.Persistance.Repositories
{
    internal class GenericRepository<TEntity> : IConcreteGenericRepository<TEntity>, IQueryableGenericRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _entities;
        private Func<IQueryable<TEntity>, IQueryable<TEntity>> _trackingFunc = _ => _;

        public GenericRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _entities = _dbContext.Set<TEntity>();
        }

        public void DisableTracking()
        {
            CheckIsTrackingEnabled();

            _trackingFunc = _ => _.AsNoTracking();
        }

        public void EnableTracking()
        {
            _trackingFunc = _ => _.AsTracking();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _trackingFunc(_entities);
        }

        public IQueryable<TEntity> WhereAsQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _trackingFunc(_entities).Where(predicate);
        }

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken ct)
        {
            return await _trackingFunc(_entities).ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _trackingFunc(_entities).SingleOrDefaultAsync(x => x.Id.Equals(id), ct);
        }

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _trackingFunc(_entities).SingleOrDefaultAsync(predicate, ct);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _trackingFunc(_entities).FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _trackingFunc(_entities).Where(predicate).ToListAsync(ct);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await _trackingFunc(_entities).AnyAsync(predicate, ct);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            CheckIsTrackingEnabled();

            await _entities.AddAsync(entity, ct);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        {
            CheckIsTrackingEnabled();

            await _entities.AddRangeAsync(entities, ct);
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            CheckIsTrackingEnabled();

            return _entities.Update(entity).Entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            CheckIsTrackingEnabled();

            _entities.UpdateRange(entities);
            return entities;
        }

        public void Remove(TEntity entity)
        {
            CheckIsTrackingEnabled();

            _entities.Remove(entity);
        }

        public void RemoveWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            CheckIsTrackingEnabled();

            var entitiesForRemove = _entities.Where(predicate);

            _entities.RemoveRange(entitiesForRemove);
        }

        private void CheckIsTrackingEnabled()
        {
            if (_dbContext.ChangeTracker.HasChanges())
                throw new InvalidOperationException("Cannot disable tracking, when tracker has changes");
        }
    }
}
