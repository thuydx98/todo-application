using Dekra.Todo.Api.Business.WorkItem.DTO;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem
{
    public sealed record UpdateWorkItemCommand(UpdateWorkItemRequestDto WorkItem, string UserId) : IRequest<ApiResult>;
}
