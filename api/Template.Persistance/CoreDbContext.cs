using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Template.Application.Core.Abstractions.Data;
using Template.Persistance.Extensions;

namespace Template.Persistance
{
    public class CoreDbContext : DbContext, IUnitOfWork
    {
        public CoreDbContext(DbContextOptions options)
            : base(options)
        {

        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyUtcDateTimeConverter();

            base.OnModelCreating(modelBuilder);
        }
    }
}
