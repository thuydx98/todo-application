using Dekra.Todo.Api.Business.WorkItem.ViewModels;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using MediatR;

namespace Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, ApiResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateWorkItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ApiResult> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
