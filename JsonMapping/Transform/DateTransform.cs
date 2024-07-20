using System.Globalization;
using System.Text.Json;

namespace JsonMapping.Transform
{
    internal class DateTransform : ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters)
        {
            if (parameters.Count != 1 || !parameters.TryGetValue("Format", out object? format))
                throw new ArgumentException("Le transform Date a besoin d'un argument : Format");

            if (format is JsonElement jsonFormat)
                format = jsonFormat.Deserialize<string>();

            if (source is string sourceStr && DateTime.TryParseExact(sourceStr, (string?)format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            return null;
        }
    }
}
