namespace Template.Domain.Entities.Abstract
{
    public abstract class EntityBase
    {
        protected EntityBase() { }

        public Guid Id { get; set; }
    }
}
