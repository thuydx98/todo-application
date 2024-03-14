using System.Text.Json.Serialization;
using System.Text.Json;

namespace Dekra.Todo.Api.Infrastructure.Utilities.Converters
{
    public class JsonStringDateTimeConverter : JsonConverter<DateTime>
    {
        /// <inheritdoc/>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateTime = reader.GetString();
            if (dateTime == null)
            {
                return default;
            }

            return DateTime.Parse(dateTime).ToUniversalTime();
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}
