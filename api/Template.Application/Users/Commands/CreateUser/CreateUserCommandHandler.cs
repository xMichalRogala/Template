using Template.Application.Core.Abstractions.Messages;

namespace Template.Application.Users.Commands.CreateUser
{
    //todo finish
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
