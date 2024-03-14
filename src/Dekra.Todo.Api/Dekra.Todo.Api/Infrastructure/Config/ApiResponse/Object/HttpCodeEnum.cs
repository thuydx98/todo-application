using System.ComponentModel;

namespace Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object
{
    public enum HttpCodeEnum
    {
        [Description("Bad request")]
        BadRequest = 400,

        [Description("Unauthorized")]
        Unauthorized = 401,

        [Description("Forbidden")]
        Forbidden = 403,

        [Description("Not found")]
        Notfound = 404,

        [Description("Internal server error")]
        InternalServerError = 500,

        [Description("OK")]
        OK = 200,
    }
}
