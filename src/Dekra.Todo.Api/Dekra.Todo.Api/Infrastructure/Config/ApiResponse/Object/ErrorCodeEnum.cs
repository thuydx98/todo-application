using System.ComponentModel;

namespace Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object
{
    public enum ErrorCodeEnum
    {
        [Description("The request is missing data or properties")]
        BAD_REQUEST = 1000,

        [Description("Cannot find any work item with Request ID")]
        NOT_EXIST_WORK_ITEM_ID = 1001,

        [Description("Work item was deleted and cannot be update")]
        DELETED_WORK_ITEM = 1002,

        [Description("Missing content of work item")]
        MISSING_CONTENT_WORK_ITEM = 1003,
    }
}
