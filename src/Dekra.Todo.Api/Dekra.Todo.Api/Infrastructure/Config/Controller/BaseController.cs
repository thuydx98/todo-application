using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dekra.Todo.Api.Infrastructure.Config.Controller
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private const string NameIdentifierClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        private readonly IMediator mediator;
        private readonly IHttpContextAccessor context;

        public BaseController(IMediator mediator, IHttpContextAccessor context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        protected IMediator Mediator => mediator;

        public string? Auth0UserId => context.HttpContext?.User.FindFirst(NameIdentifierClaim)?.Value;
    }
}
