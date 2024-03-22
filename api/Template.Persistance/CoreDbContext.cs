using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Template.Application.Core.Abstractions.Commons;
using Template.Application.Core.Abstractions.Data;
using Template.Domain.Entities;
using Template.Domain.Entities.Abstract;
using Template.Persistance.Extensions;

namespace Template.Persistance
{
    public class CoreDbContext(DbContextOptions options, IDateTime dateTime) : DbContext(options), IUnitOfWork
    {
        private readonly IDateTime _dateTime = dateTime;

        public DbSet<User> Users { get; set; }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task RunInTransactionAsync(Func<Task> action, CancellationToken ct)
        {
            await using var transaction = await Database.BeginTransactionAsync(ct);
            try
            {
                await action();

                await transaction.CommitAsync(ct);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }

        public async Task<T> RunInTransactionAsync<T>(Func<Task<T>> action, CancellationToken ct)
        {
            await using var transaction = await Database.BeginTransactionAsync(ct);
            try
            {
                var result = await action();

                await transaction.CommitAsync(ct);

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //nice-to-have: remove to interceptor
        {
            DateTime utcNow = _dateTime.UtcNow;

            UpdateAuditableEntities(utcNow);

            UpdateSoftDeletableEntities(utcNow);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyUtcDateTimeConverter();

            base.OnModelCreating(modelBuilder);
        }

        private void UpdateAuditableEntities(DateTime utcNow) //nice-to-have: remove to interceptor
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
                }
            }
        }

        private void UpdateSoftDeletableEntities(DateTime utcNow) //nice-to-have: remove to interceptor
        {
            foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
            {
                if (entityEntry.State != EntityState.Deleted)
                {
                    continue;
                }

                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

                entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
            }
        }

        private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry) //nice-to-have: remove to interceptor
        {
            if (!entityEntry.References.Any())
            {
                return;
            }

            foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry!.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry!.State = EntityState.Unchanged;

                UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
            }
        }
    }
}
