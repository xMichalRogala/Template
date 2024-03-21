using Template.Domain.Utilities;

namespace Template.Api.Contracts
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
