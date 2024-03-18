using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Config.ApiResponse;
using Dekra.Todo.Api.Infrastructure.Config.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dekra.Todo.Api.Business.WorkItem.Queries.GetWorkItemsByUser;
using Dekra.Todo.Api.Business.WorkItem.Commands.DeleteWorkItem;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using Dekra.Todo.Api.Business.WorkItem.Commands.UpdateWorkItem;
using Dekra.Todo.Api.Business.WorkItem.Commands.CreateWorkItem;

namespace Dekra.Todo.Api.Controllers
{
    [Route("api/work-items")]
    [ApiController]
    public class WorkItemsController(IMediator mediator, IHttpContextAccessor context) : BaseController(mediator, context)
    {
        [HttpGet]
        public async Task<IActionResult> GetWorkItems()
        {
            if (this.Auth0UserId.IsEmpty())
            {
                return ApiResult.Failed(HttpCodeEnum.Unauthorized);
            }

            return await this.Mediator.Send(new GetWorkItemsByUserQuery(this.Auth0UserId!));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkItem(CreateWorkItemRequestModel workItem)
        {
            if (this.Auth0UserId.IsEmpty())
            {
                return ApiResult.Failed(HttpCodeEnum.Unauthorized);
            }

            return await this.Mediator.Send(new CreateWorkItemCommand(WorkItem: workItem, UserId: this.Auth0UserId!));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkItem(Guid id, UpdateWorkItemRequestModel workItem)
        {
            if (this.Auth0UserId.IsEmpty())
            {
                return ApiResult.Failed(HttpCodeEnum.Unauthorized);
            }

            return await this.Mediator.Send(new UpdateWorkItemCommand(WorkItemId: id, WorkItem: workItem, UserId: this.Auth0UserId!));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkItem(Guid id)
        {
            if (this.Auth0UserId.IsEmpty())
            {
                return ApiResult.Failed(HttpCodeEnum.Unauthorized);
            }

            return await this.Mediator.Send(new DeleteWorkItemCommand(WorkItemId: id, UserId: this.Auth0UserId!));
        }
    }
}
