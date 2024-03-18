using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem
{
    public sealed record UpdateWorkItemCommand(Guid WorkItemId, UpdateWorkItemRequestModel WorkItem, string UserId) : IRequest<ApiResult>;

    public class UpdateWorkItemRequestModel
    {
        public string Content { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
