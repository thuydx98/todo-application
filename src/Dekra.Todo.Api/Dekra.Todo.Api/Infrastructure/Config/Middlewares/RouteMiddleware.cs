namespace Dekra.Todo.Api.Infrastructure.Config.Middlewares
{
    public class RouteMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            await next(context);

            if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Request.Path = "/index.html";
                await next(context);
            }
        }
    }
}
