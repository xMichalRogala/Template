using Template.Domain.Utilities;

namespace Template.Application.Core.Errors
{
    internal static class ValidationErrors
    {
        internal static class CreateUser
        {
            internal static Error FirstNameIsRequired => new Error("CreateUser.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired => new Error("CreateUser.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired => new Error("CreateUser.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new Error("CreateUser.PasswordIsRequired", "The password is required.");
        }
    }
}
