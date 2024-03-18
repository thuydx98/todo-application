using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ApiResult> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.UserId.IsEmpty() || request.WorkItem == null)
            {
                return ApiResult.Failed(ErrorCodeEnum.BAD_REQUEST);
            }

            if (request.WorkItem.Content.IsEmpty())
            {
                return ApiResult.Failed(ErrorCodeEnum.MISSING_CONTENT_WORK_ITEM);
            }

            var workItem = new Data.Entities.WorkItem
            {
                UserId = request.UserId,
                Content = request.WorkItem.Content,
            };

            unitOfWork.GetRepository<Data.Entities.WorkItem>().Insert(workItem);

            await unitOfWork.CommitAsync();

            return ApiResult.Succeeded(workItem);
        }
    }
}
