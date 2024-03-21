using Template.Domain.Utilities;

namespace Template.Application.Core.Errors
{
    internal static class ValidationErrors
    {
        internal static class CreateUser
        {
            internal static Error FirstNameIsRequired => new("CreateUser.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired => new("CreateUser.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired => new("CreateUser.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new("CreateUser.PasswordIsRequired", "The password is required.");
        }
    }
}
