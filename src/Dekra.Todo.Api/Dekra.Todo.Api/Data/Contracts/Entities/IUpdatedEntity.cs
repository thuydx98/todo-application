namespace Dekra.Todo.Api.Data.Contracts.Entities
{
    public interface IUpdatedEntity
    {
        DateTime? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
    }
}
