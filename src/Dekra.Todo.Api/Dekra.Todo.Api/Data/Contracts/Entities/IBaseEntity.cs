namespace Dekra.Todo.Api.Data.Contracts.Entities
{
    public interface IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
