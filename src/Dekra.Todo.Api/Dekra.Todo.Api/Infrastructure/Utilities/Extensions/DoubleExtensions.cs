namespace Dekra.Todo.Api.Infrastructure.Utilities.Extensions
{
    public static class DoubleExtensions
    {
        public static DateTime ToDateTime(this double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp);
        }
    }
}
