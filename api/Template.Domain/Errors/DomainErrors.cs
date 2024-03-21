using Template.Domain.Utilities;

namespace Template.Domain.Errors
{
    public static class DomainErrors
    {
        public static class General
        {
            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        public static class FirstName
        {
            public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");

            public static Error LongerThanAllowed(int limit) => new Error("FirstName.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");
        }

        public static class LastName
        {
            public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");

            public static Error LongerThanAllowed(int limit) => new Error("LastName.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");
        }

        public static class Email
        {
            public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

            public static Error LongerThanAllowed(int limit) => new Error("Email.LongerThanAllowed", $"The first name is longer than allowed (${limit}).");

            public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
        }
    }
}
