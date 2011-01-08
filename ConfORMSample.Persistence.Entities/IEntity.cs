namespace ConfORMSample.Persistence.Entities
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}