using FluentValidation;
using Template.Domain.Utilities;

namespace Template.Application.Core.Extensions
{
    internal  static class FluentValidationxEtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            if (error is null)
            {
                throw new ArgumentNullException(nameof(error), "The error is required");
            }

            return rule.WithErrorCode(error.Code).WithMessage(error.Message);
        }
    }
}
