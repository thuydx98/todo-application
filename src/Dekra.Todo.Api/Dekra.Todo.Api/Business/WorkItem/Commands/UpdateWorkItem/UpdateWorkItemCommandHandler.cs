using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ApiResult> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.UserId.IsEmpty() || request.WorkItemId == Guid.Empty || request.WorkItem == null || request.WorkItem.Content.IsEmpty())
            {
                return ApiResult.Failed(ErrorCodeEnum.BAD_REQUEST);
            }

            var workItem = await unitOfWork.GetRepository<Data.Entities.WorkItem>().FirstOrDefaultAsync(
                predicate: s => s.Id == request.WorkItemId && s.UserId == request.UserId,
                cancellationToken: cancellationToken);

            if (workItem == null)
            {
                return ApiResult.Failed(ErrorCodeEnum.NOT_EXIST_WORK_ITEM_ID, HttpCodeEnum.Notfound);
            }

            if (workItem.IsDeleted)
            {
                return ApiResult.Failed(ErrorCodeEnum.DELETED_WORK_ITEM);
            }

            workItem.Content = request.WorkItem.Content;
            workItem.IsCompleted = request.WorkItem.IsCompleted;

            unitOfWork.GetRepository<Data.Entities.WorkItem>().Update(workItem);

            await unitOfWork.CommitAsync();

            return ApiResult.Succeeded(workItem);
        }
    }
}
