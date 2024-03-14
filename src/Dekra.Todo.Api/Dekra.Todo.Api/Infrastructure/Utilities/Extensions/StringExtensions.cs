namespace Dekra.Todo.Api.Infrastructure.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string? s) => s == null || s.Trim().Length == 0;
        public static bool IsNotEmpty(this string s) => s != null && s.Trim().Length > 0;

        public static object? ToEnum<TResult>(this string s) where TResult : Enum
        {
            if (s.IsNotEmpty() && Enum.TryParse(typeof(TResult), s, true, out object? value))
            {
                if (Enum.IsDefined(typeof(TResult), value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}
