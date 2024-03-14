using System.ComponentModel;

namespace Dekra.Todo.Api.Infrastructure.Config.ApiResponse.Object
{
    public enum ErrorCodeEnum
    {
        [Description("Invalid witch")]
        INVALID_WITCH = 1000,

        [Description("Empty id")]
        EMPTY_ID = 400,

        [Description("Transaction is not existed or completed")]
        INVALID_TRANSACTION = 1001,

        [Description("User is not existing")]
        NOT_EXISTING_USER = 1002,

        [Description("The gift has been given for another transaction")]
        GIFT_HAS_BEEN_GIVEN = 1003,
    }
}
