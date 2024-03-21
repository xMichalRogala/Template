using Template.Application.Core.Abstractions.Commons;

namespace Template.Infrastructure.Core.Commons
{
    internal sealed class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
