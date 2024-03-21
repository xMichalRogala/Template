using Template.Application.Core.Abstractions.Messages;

namespace Template.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : ICommand<Guid>
    {
    }
}
