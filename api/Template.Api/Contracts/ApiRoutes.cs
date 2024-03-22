namespace Template.Api.Contracts
{
    internal static class ApiRoutes
    {
        public static class Authentication
        {
            public const string Login = "auth/login";

            public const string Register = "auth/register";

            public const string RefreshToken = "auth/refresh-token";

            public const string Revoke = "auth/revoke";
        }
    }
}
