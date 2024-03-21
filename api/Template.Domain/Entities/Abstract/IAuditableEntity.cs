namespace Template.Domain.Entities.Abstract
{
    public interface IAuditableEntity
    {
        DateTime CreatedOnUtc { get; }

        DateTime? ModifiedOnUtc { get; }
    }
}
