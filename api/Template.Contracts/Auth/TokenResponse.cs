namespace Template.Contracts.Auth
{
    public sealed record TokenResponse(string Token, string RefreshToken, DateTime TokenExpiryTime)
    {
    }
}
