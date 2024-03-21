using System.Text.RegularExpressions;
using Template.Domain.Errors;
using Template.Domain.Utilities;

namespace Template.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public const int MaxLength = 256;

        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        private static readonly Lazy<Regex> EmailFormatRegex =
            new(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        private Email(string value) => Value = value;

        public static Email Create(string email)
        {
            Ensure.That(email, f => !string.IsNullOrWhiteSpace(f), DomainErrors.Email.NullOrEmpty);
            Ensure.That(email, f => f.Length <= MaxLength, DomainErrors.Email.LongerThanAllowed(MaxLength));
            Ensure.That(email, f => EmailFormatRegex.Value.IsMatch(f), DomainErrors.Email.LongerThanAllowed(MaxLength));

            return new Email(email);
        }

        public string Value { get; }

        public static implicit operator string(Email email) => email.Value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
