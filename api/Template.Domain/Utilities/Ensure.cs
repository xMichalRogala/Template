using Template.Domain.Exceptions;

namespace Template.Domain.Utilities
{
    public static class Ensure
    {
        public static void That<T>(T value, Func<T, bool> prediction, Error error)
        {
            if(!prediction(value))
            {
                throw new DomainException(error);
            }
        }

        public static void NotEmpty(string value, string message, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void NotEmpty(Guid value, string message, string argumentName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void NotEmpty(DateTime value, string message, string argumentName)
        {
            if (value == default)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void NotNull<T>(T value, string message, string argumentName)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }
    }
}
