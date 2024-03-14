using System.ComponentModel;

namespace Dekra.Todo.Api.Infrastructure.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : string.Empty;
        }
    }
}
