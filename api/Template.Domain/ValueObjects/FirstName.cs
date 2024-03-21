using Template.Domain.Errors;
using Template.Domain.Utilities;

namespace Template.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 100;

        public string Value { get; set; }

        private FirstName(string value) => Value = value;

        public static FirstName Create(string firstName)
        {
            Ensure.That(firstName ,f => !string.IsNullOrWhiteSpace(f), DomainErrors.FirstName.NullOrEmpty);
            Ensure.That(firstName, f => f.Length <= MaxLength, DomainErrors.FirstName.LongerThanAllowed(MaxLength));

            return new FirstName(firstName);
        }

        public static implicit operator string(FirstName firstName) => firstName.Value;

        public override string ToString() => Value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
