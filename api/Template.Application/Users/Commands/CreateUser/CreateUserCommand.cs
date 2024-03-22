using Template.Application.Core.Abstractions.Messages;
using Template.Contracts.Auth;

namespace Template.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : ICommand<TokenResponse>
    {
    }
}
