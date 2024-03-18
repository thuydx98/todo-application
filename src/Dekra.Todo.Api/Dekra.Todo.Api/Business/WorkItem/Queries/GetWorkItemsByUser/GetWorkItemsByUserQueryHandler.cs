using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Queries.GetWorkItemsByUser
{
    public class GetWorkItemsByUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetWorkItemsByUserQuery, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ApiResult> Handle(GetWorkItemsByUserQuery request, CancellationToken cancellationToken)
        {
            var workItems = await this.unitOfWork.GetRepository<Data.Entities.WorkItem>().GetListAsync<WorkItemViewModel>(
                selector: n => new WorkItemViewModel()
                {
                    Id = n.Id,
                    Content = n.Content,
                    CreatedAt = n.CreatedAt,
                    IsCompleted = n.IsCompleted,
                },
                predicate: x => x.UserId == request.UserId && !x.IsDeleted,
                orderBy: x => x.OrderByDescending(o => o.CreatedAt),
                cancellationToken: cancellationToken);

            return ApiResult.Succeeded(workItems);
        }
    }
}
