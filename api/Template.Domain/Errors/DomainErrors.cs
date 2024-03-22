using Template.Domain.Utilities;

namespace Template.Domain.Errors
{
    public static class DomainErrors
    {
        public static class General
        {
            public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
        }

        public static class FirstName
        {
            public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

            public static Error LongerThanAllowed(int limit) => new("FirstName.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");
        }

        public static class LastName
        {
            public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

            public static Error LongerThanAllowed(int limit) => new("LastName.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");
        }

        public static class Email
        {
            public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");

            public static Error LongerThanAllowed(int limit) => new("Email.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");

            public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
        }

        public static class Password
        {
            public static Error NullOrEmpty => new("Password.NullOrEmpty", "The password is required.");

            public static Error TooShort => new("Password.TooShort", "The password is too short.");

            public static Error MissingUppercaseLetter => new(
                "Password.MissingUppercaseLetter",
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new(
                "Password.MissingLowercaseLetter",
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new(
                "Password.MissingDigit",
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new(
                "Password.MissingNonAlphaNumeric",
                "The password requires at least one non-alphanumeric.");
        }

        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");
        }
    }
}
