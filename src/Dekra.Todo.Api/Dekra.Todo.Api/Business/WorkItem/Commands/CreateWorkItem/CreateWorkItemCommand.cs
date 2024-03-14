using Dekra.Todo.Api.Business.WorkItem.DTO;
using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem
{
    public sealed record CreateWorkItemCommand(CreateWorkItemRequestDto WorkItem, string UserId) : IRequest<ApiResult>;
}
