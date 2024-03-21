using FluentValidation.Results;
using Template.Domain.Utilities;

namespace Template.Application.Core.Exceptions
{
    public sealed class ValidationException(IEnumerable<ValidationFailure> failures) : Exception("One or more validation failures has occurred.")
    {
        public IReadOnlyCollection<Error> Errors { get; } = failures
               .Distinct()
               .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
               .ToList();
    }
}
