using Template.Application.Core.Abstractions.Messages;
using Template.Contracts.Auth;

namespace Template.Application.Auth.Login
{
    public sealed class LoginCommand : ICommand<TokenResponse>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
