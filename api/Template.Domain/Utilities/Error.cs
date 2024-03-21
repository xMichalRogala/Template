
namespace Template.Domain.Utilities
{
    public sealed class Error(string code, string message) : ValueObject
    {
        public string Code { get; } = code;
        public string Message { get; } = message;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Message;
        }

        public static implicit operator string(Error error) => error?.Code ?? string.Empty;
    }
}
