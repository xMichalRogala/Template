using Template.Domain.Entities;

namespace Template.Application.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        (string token, DateTime tokenExpirationTime) Create(User user, bool refreshToken = false);
    }
}
