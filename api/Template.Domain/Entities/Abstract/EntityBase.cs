using Template.Domain.Utilities;

namespace Template.Domain.Entities.Abstract
{
    public abstract class EntityBase : IEquatable<EntityBase>
    {
        protected EntityBase(Guid id)
        {
            Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

            Id = id;
        }

        protected EntityBase() { }

        public Guid Id { get; set; }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b) => !(a == b);

        public bool Equals(EntityBase? other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (!(obj is EntityBase other))
            {
                return false;
            }

            if (Id == Guid.Empty || other.Id == Guid.Empty)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
