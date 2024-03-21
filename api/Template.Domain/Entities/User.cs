using Template.Domain.Entities.Abstract;
using Template.Domain.Utilities;
using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class User : EntityBase, IAuditableEntity, ISoftDeletableEntity
    {
        private string _passwordHash;

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

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public Email Email { get; private set; }

        public DateTime CreatedOnUtc { get; }

        public DateTime? ModifiedOnUtc { get; }

        public DateTime? DeletedOnUtc { get; }

        public bool Deleted { get; }

        public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash) => new User(firstName, lastName, email, passwordHash);
    }
}
