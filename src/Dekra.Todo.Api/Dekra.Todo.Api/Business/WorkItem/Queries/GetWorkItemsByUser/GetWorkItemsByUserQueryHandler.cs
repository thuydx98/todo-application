using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Queries.GetWorkItemsByUser
{
    public class GetWorkItemsByUserQueryHandler : IRequestHandler<GetWorkItemsByUserQuery, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetWorkItemsByUserQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResult> Handle(GetWorkItemsByUserQuery request, CancellationToken cancellationToken)
        {
            var transactions = await this.unitOfWork.GetRepository<Data.Entities.WorkItem>().GetListAsync<WorkItemViewModel>(
                selector: n => new WorkItemViewModel()
                {
                    Id = n.Id,
                },
                orderBy: x => x.OrderByDescending(o => o.CreatedAt),
                cancellationToken: cancellationToken);

            return ApiResult.Succeeded(transactions);
        }
    }
}
