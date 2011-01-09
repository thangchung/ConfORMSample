namespace ConfORMSample.Common.Entities.Contract
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}