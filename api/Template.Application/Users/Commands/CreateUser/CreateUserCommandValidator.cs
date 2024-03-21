using FluentValidation;
using Template.Application.Core.Errors;
using Template.Application.Core.Extensions;

namespace Template.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithError(ValidationErrors.CreateUser.FirstNameIsRequired);

            RuleFor(x => x.LastName).NotEmpty().WithError(ValidationErrors.CreateUser.LastNameIsRequired);

            RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.CreateUser.EmailIsRequired);

            RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired);
        }
    }
}
