using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateWorkItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ApiResult> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
