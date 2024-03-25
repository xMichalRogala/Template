using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.ValueObjects;

namespace Template.Persistance.Repositories
{
    internal sealed class UserRepository(CoreDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct) => await FirstOrDefaultAsync(x => x.Email == email, ct);

        public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken ct) => !await AnyAsync(x => x.Email == email, ct);
    }
}
