using System;
using System.Text.Json;

namespace Microsoft.AspNetCore.Mvc
{
    public static class JsonOptionsExtensions
    {
        public static JsonOptions SerializeCamelCase(this JsonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            return options;
        }

        public static JsonOptions DeserializeCaseInsensitive(this JsonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

            return options;
        }

        public static JsonOptions IgnoreNullValues(this JsonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.JsonSerializerOptions.IgnoreNullValues = true;

            return options;
        }
    }
}
