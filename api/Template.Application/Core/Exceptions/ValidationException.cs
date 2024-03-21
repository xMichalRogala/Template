using FluentValidation.Results;
using Template.Domain.Utilities;

namespace Template.Application.Core.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationFailure> failures)
           : base("One or more validation failures has occurred.") =>
           Errors = failures
               .Distinct()
               .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
               .ToList();

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
