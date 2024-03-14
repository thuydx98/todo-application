namespace Dekra.Todo.Api.Business.WorkItem.ViewModels
{
    public class WorkItemViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
