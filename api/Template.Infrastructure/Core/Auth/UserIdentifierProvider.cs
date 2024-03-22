using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Template.Application.Core.Abstractions.Auth;

namespace Template.Infrastructure.Core.Auth
{
    internal sealed class UserIdentifierProvider : IUserIdentifierProvider
    {
        public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
        {
            string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtProvider.UserIdClaimName)
                ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

            UserId = new Guid(userIdClaim);
        }

        public Guid UserId { get; }
    }
}
