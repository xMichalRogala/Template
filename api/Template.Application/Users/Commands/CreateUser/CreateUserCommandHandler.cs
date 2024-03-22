using System.Reflection.Metadata.Ecma335;
using Template.Application.Core.Abstractions.Auth;
using Template.Application.Core.Abstractions.Cryptography;
using Template.Application.Core.Abstractions.Data;
using Template.Application.Core.Abstractions.Messages;
using Template.Contracts.Auth;
using Template.Domain.Entities;
using Template.Domain.Errors;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;
using Template.Domain.ValueObjects;

namespace Template.Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider) : ICommandHandler<CreateUserCommand, TokenResponse>
    {
        public async Task<TokenResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var firstName = FirstName.Create(request.FirstName);
            var lastName = LastName.Create(request.LastName);
            var email = Email.Create(request.Email);
            var password = Password.Create(request.Password);

            if (!await userRepository.IsEmailUniqueAsync(email, cancellationToken))
            {
                throw new DomainException(DomainErrors.User.DuplicateEmail);
            }

            string passwordHash = passwordHasher.HashPassword(password);

            var user = User.Create(firstName,lastName,email, passwordHash);

            await userRepository.AddAsync(user, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var accessTokenResult = jwtProvider.Create(user);
            var refreshTokenResult = jwtProvider.Create(user, true);

            return new TokenResponse(accessTokenResult.token, refreshTokenResult.token, accessTokenResult.tokenExpirationTime);
        }
    }
}
