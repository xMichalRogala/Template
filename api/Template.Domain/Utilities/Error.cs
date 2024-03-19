
namespace Template.Domain.Utilities
{
    public sealed class Error : ValueObject
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Message;
        }

        public static implicit operator string(Error error) => error?.Code ?? string.Empty;
    }
}
