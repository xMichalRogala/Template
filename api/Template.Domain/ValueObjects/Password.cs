using Template.Domain.Errors;
using Template.Domain.Utilities;

namespace Template.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        private const int MinPasswordLength = 6;
        private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
        private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
        private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
        private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));

        private Password(string value) => Value = value;

        public string Value { get; }

        public static implicit operator string(Password password) => password?.Value ?? string.Empty;

        public static Password Create(string password)
        {
            Ensure.That(password, p => !string.IsNullOrWhiteSpace(p), DomainErrors.Email.NullOrEmpty);
            Ensure.That(password, p => p.Length >= MinPasswordLength, DomainErrors.Password.TooShort);
            Ensure.That(password, p => p.Any(IsLower), DomainErrors.Password.MissingLowercaseLetter);
            Ensure.That(password, p => p.Any(IsUpper), DomainErrors.Password.MissingUppercaseLetter);
            Ensure.That(password, p => p.Any(IsDigit), DomainErrors.Password.MissingDigit);
            Ensure.That(password, p => p.Any(IsNonAlphaNumeric), DomainErrors.Password.MissingNonAlphaNumeric);

            return new Password(password);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
