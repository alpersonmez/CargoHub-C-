using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

// namespace Cargohub.DataConverters
// {
    // public class DateTimeConverter : DateTimeConverterBase
    // {
    //     private readonly string[] _formats = new[]
    //     {
    //         "yyyy-MM-dd HH:mm:ss",
    //         "yyyy-MM-ddTHH:mm:ssZ",
    //         "MM/dd/yyyy hh:mm:ss tt",
    //         "MM/dd/yyyy hh:mm:ss TT"
    //     };

    //     public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //     {
    //         writer.WriteValue(((DateTime)value).ToString(_formats[0]));
    //     }

    //     public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //     {
    //         if (reader.TokenType == JsonToken.Null)
    //         {
    //             return null; // Handle null values gracefully
    //         }

    //         if (reader.TokenType == JsonToken.Date)
    //         {
    //             return reader.Value; // Handle DateTime tokens
    //         }

    //         if (reader.TokenType == JsonToken.Integer)
    //         {
    //             // Handle Unix timestamp as integer
    //             long timestamp = (long)reader.Value;
    //             return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
    //         }

    //         if (reader.TokenType == JsonToken.String)
    //         {
    //             string dateString = (string)reader.Value;

    //             // Attempt to parse using the predefined formats
    //             if (DateTime.TryParseExact(dateString, _formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
    //             {
    //                 return date;
    //             }

    //             // Attempt to parse as a fallback general format
    //             if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
    //             {
    //                 return date;
    //             }

    //             // Log or output details about the unexpected input
    //             throw new JsonSerializationException($"Invalid date format: {dateString}");
    //         }

    //         throw new JsonSerializationException($"Unexpected token type: {reader.TokenType} at path {reader.Path}");
    //     }
    // }
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace Cargohub.DataConverters
{
    public class DateTimeConverters : IsoDateTimeConverter
    {
        public DateTimeConverters()
        {
            DateTimeStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var dateStr = reader.Value?.ToString();
            if (string.IsNullOrEmpty(dateStr))
                return null;

            var formats = new[]
            {
                "yyyy-MM-dd HH:mm:ss", "yyyy-MM-ddTHH:mm:ssZ", "MM/dd/yyyy hh:mm:ss tt",
                "MM/dd/yyyy hh:mm:ss TT", "MM/dd/yyyy h:mm:ss tt", "MM/d/yyyy hh:mm:ss tt",
                "MM/d/yyyy h:mm:ss tt", "MM/dd/yyyy h:mm:ss TT"
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
                return DateTime.SpecifyKind(date, DateTimeKind.Utc);

            Console.WriteLine($"Invalid date format: {dateStr}"); // Log invalid date
            return null; // Return null instead of throwing an exception
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime dateTime)
            {
                writer.WriteValue(dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            }
        }
    }
    }

