using Template.Domain.Entities;

namespace Template.Application.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string Create(User user);
    }
}
