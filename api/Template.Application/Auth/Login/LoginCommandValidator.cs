using FluentValidation;
using Template.Application.Core.Errors;
using Template.Application.Core.Extensions;

namespace Template.Application.Auth.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.Login.EmailIsRequired);

            RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.Login.PasswordIsRequired);
        }
    }
}
