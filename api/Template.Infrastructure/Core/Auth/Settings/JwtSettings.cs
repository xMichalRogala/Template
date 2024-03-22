namespace Template.Infrastructure.Core.Auth.Settings
{
    public class JwtSettings
    {
        public const string SettingsKey = "Jwt";
        public const string RefreshJwtTokenSchema = "RefreshJwtTokenSchema";

        public JwtSettings()
        {
            Issuer = string.Empty;
            Audience = string.Empty;
            SecurityAccessTokenKey = string.Empty;
            SecurityRefreshTokenKey = string.Empty;
        }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityAccessTokenKey { get; set; }

        public string SecurityRefreshTokenKey { get; set; }

        public int TokenExpirationInMinutes { get; set; }

        public int TokenRefreshExpirationInMinutes { get; set; }
    }
}
