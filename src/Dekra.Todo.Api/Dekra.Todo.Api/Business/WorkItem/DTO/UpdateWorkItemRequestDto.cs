namespace Dekra.Todo.Api.Business.WorkItem.DTO
{
    public class UpdateWorkItemRequestDto : CreateWorkItemRequestDto
    {
        public Guid Id { get; set; }
    }
}
