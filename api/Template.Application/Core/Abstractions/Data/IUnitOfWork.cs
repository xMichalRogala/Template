using Microsoft.EntityFrameworkCore.Storage;

namespace Template.Application.Core.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task RunInTransactionAsync(Func<Task> action, CancellationToken ct);

        Task<T> RunInTransactionAsync<T>(Func<Task<T>> action, CancellationToken ct);
    }
}
