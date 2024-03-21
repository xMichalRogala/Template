using Template.Domain.Errors;
using Template.Domain.Utilities;

namespace Template.Domain.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public const int MaxLength = 100;

        public string Value { get; set; }

        private LastName(string value) => Value = value;

        public static LastName Create(string lastName)
        {
            Ensure.That(lastName, f => !string.IsNullOrWhiteSpace(f), DomainErrors.LastName.NullOrEmpty);
            Ensure.That(lastName, f => f.Length <= MaxLength, DomainErrors.LastName.LongerThanAllowed(MaxLength));

            return new LastName(lastName);
        }

        public static implicit operator string(LastName lastName) => lastName.Value;

        public override string ToString() => Value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
