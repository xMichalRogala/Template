using Template.Domain.Utilities;

namespace Template.Domain.Exceptions
{
    public class DomainException(Error error) : Exception(error.Message)
    {
        public Error Error { get; } = error;
    }
}
