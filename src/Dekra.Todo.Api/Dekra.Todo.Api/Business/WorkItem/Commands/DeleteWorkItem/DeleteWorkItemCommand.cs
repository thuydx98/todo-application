using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.DeleteWorkItem
{
    public sealed record DeleteWorkItemCommand(Guid WorkItemId, string UserId) : IRequest<ApiResult>;
}
