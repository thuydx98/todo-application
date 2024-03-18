using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem
{
    public sealed record CreateWorkItemCommand(CreateWorkItemRequestModel WorkItem, string UserId) : IRequest<ApiResult>;

    public class CreateWorkItemRequestModel
    {
        public string Content { get; set; } = null!;
    }
}
