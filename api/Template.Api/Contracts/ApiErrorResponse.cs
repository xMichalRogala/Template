using Template.Domain.Utilities;

namespace Template.Api.Contracts
{
    public class ApiErrorResponse(IReadOnlyCollection<Error> errors)
    {
        public IReadOnlyCollection<Error> Errors { get; } = errors;
    }
}
