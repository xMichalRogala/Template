namespace Template.Application.Core.Abstractions.Auth
{
    public interface IUserIdentifierProvider
    {
        Guid UserId { get; }
    }
}
