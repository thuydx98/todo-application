using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ApiResult> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.UserId.IsEmpty() || request.WorkItemId == Guid.Empty)
            {
                return ApiResult.Failed(ErrorCodeEnum.BAD_REQUEST);
            }

            var workItem = await unitOfWork.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                predicate: s => s.Id == request.WorkItemId && s.UserId == request.UserId && !s.IsDeleted,
                cancellationToken: cancellationToken);

            if (workItem == null)
            {
                return ApiResult.Failed(ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID, HttpCodeEnum.Notfound);
            }

            workItem.IsDeleted = true;

            unitOfWork.GetRepository<Data.Entities.WorkItem>().Update(workItem);

            await unitOfWork.CommitAsync();

            return ApiResult.Succeeded(workItem);
        }
    }
}
