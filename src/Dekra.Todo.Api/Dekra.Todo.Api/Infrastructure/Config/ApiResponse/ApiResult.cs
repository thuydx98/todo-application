using Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object;
using Dekra.Todo.Api.Infrastructure.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Dekra.Todo.Api.Infrastructure.Config.ApiResponse
{
    public class ApiResult : IActionResult
    {
        public HttpCodeEnum HttpCode { get; private set; }
        public ApiJsonResult<object>? Value { get; private set; }

        public static ApiResult Succeeded(object? data = null) => new()
        {
            HttpCode = HttpCodeEnum.OK,
            Value = new ApiJsonResult<object>(data),
        };

        public static ApiResult Failed(HttpCodeEnum httpCode, object result = null) => new()
        {
            HttpCode = httpCode,
            Value = new ApiJsonResult<object>((int)httpCode, httpCode.GetDescription(), result),
        };

        public static ApiResult Failed(HttpCodeEnum httpCode, string errorMessage) => new()
        {
            HttpCode = httpCode,
            Value = new ApiJsonResult<object>((int)httpCode, errorMessage),
        };

        public static ApiResult Failed(ErrorCodeEnum errorCode, HttpCodeEnum httpCode = HttpCodeEnum.BadRequest) => new()
        {
            HttpCode = httpCode,
            Value = new ApiJsonResult<object>((int)errorCode, errorCode.GetDescription()),
        };

        public static ApiResult Failed(ErrorCodeEnum errorCode, object result, HttpCodeEnum httpCode = HttpCodeEnum.BadRequest) => new()
        {
            HttpCode = httpCode,
            Value = new ApiJsonResult<object>((int)errorCode, errorCode.GetDescription(), result)
        };

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpCode;

            var result = new ObjectResult(Value)
            {
                StatusCode = (int)HttpCode,
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
