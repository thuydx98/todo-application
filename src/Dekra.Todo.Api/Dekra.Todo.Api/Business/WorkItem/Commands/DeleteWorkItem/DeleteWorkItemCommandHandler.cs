using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteWorkItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ApiResult> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
