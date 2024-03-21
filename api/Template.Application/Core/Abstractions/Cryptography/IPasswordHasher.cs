using Template.Domain.ValueObjects;

namespace Template.Application.Core.Abstractions.Cryptography
{
    public interface IPasswordHasher
    {
        string HashPassword(Password password);
    }
}
