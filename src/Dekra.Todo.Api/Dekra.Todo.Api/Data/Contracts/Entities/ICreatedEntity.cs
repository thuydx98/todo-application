namespace Dekra.Todo.Api.Data.Contracts.Entities
{
    public interface ICreatedEntity
    {
        DateTime CreatedAt { get; set; }
        string? CreatedBy { get; set; }
    }
}
