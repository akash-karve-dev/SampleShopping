namespace Order.Domain
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}