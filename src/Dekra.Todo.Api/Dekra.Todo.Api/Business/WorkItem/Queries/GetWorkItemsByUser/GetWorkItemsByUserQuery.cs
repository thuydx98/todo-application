using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Queries.GetWorkItemsByUser
{
    public sealed record GetWorkItemsByUserQuery(string UserId) : IRequest<ApiResult>;
}
