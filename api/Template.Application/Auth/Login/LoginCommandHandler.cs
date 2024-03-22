using Template.Application.Core.Abstractions.Auth;
using Template.Application.Core.Abstractions.Messages;
using Template.Contracts.Auth;
using Template.Domain.Errors;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.Services;
using Template.Domain.ValueObjects;

namespace Template.Application.Auth.Login
{
    internal sealed class LoginCommandHandler(IUserRepository userRepository, IPasswordHashChecker passwordHashChecker, IJwtProvider jwtProvider) : ICommandHandler<LoginCommand, TokenResponse>
    {
        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);

            var user = await userRepository.GetByEmailAsync(email, cancellationToken) ?? throw new DomainException(DomainErrors.Authentication.InvalidEmailOrPassword);

            bool isPasswordValid = user.VerifyPasswordHash(request.Password, passwordHashChecker);

            if (!isPasswordValid)
            {
                throw new DomainException(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            var accessTokenResult = jwtProvider.Create(user);
            var refreshTokenResult = jwtProvider.Create(user, true);

            return new TokenResponse(accessTokenResult.token, refreshTokenResult.token, accessTokenResult.tokenExpirationTime);
        }
    }
}
