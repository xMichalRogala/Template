namespace Template.Domain.Entities.Abstract
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedOnUtc { get; }

        bool Deleted { get; }
    }
}
