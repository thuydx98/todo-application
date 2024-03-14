using Dekra.Todo.Api.Data.Contracts.Entities;

namespace Dekra.Todo.Api.Data.Entities
{
    public class WorkItem : IBaseEntity<Guid>, ICreatedEntity, IUpdatedEntity
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public required string Content { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
