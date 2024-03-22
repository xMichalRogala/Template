using Template.Domain.Entities;
using Template.Domain.ValueObjects;

namespace Template.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);

        Task<User?> GetByEmailAsync(Email email, CancellationToken ct);

        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken ct);

        Task<User> AddAsync(User user, CancellationToken ct);
    }
}
