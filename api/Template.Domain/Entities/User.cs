using Template.Domain.Entities.Abstract;
using Template.Domain.Services;
using Template.Domain.Utilities;
using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class User : EntityBase, IAuditableEntity, ISoftDeletableEntity
    {
        private readonly string _passwordHash;

        private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
            : base(Guid.NewGuid())
        {
            Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
            Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));
            Ensure.NotEmpty(email, "The email is required.", nameof(email));
            Ensure.NotEmpty(passwordHash, "The password hash is required", nameof(passwordHash));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            _passwordHash = passwordHash;
        }

        #pragma warning disable CS8618
        //ef core
        private User() { }
        #pragma warning restore CS8618

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public Email Email { get; private set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public string? RefreshToken { get; private set; }

        public DateTime? RefreshTokenExpiryTime { get; private set; }

        public bool Deleted { get; }

        public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash) => new(firstName, lastName, email, passwordHash);

        public void AddRefreshToken(string refreshToken, DateTime refreshTokenExpiryTime)
        {
            Ensure.NotEmpty(refreshTokenExpiryTime, "The refresh token expiry time is required.", nameof(refreshTokenExpiryTime));
            Ensure.NotEmpty(refreshToken, "The refresh token is required.", nameof(refreshToken));

            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
            => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);
    }
}
