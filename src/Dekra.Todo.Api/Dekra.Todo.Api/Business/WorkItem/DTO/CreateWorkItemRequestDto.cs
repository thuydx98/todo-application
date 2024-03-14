namespace Dekra.Todo.Api.Business.WorkItem.DTO
{
    public class CreateWorkItemRequestDto
    {
        public string Content { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
