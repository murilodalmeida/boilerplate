using System.Text.Json;
using System.Text.Json.Serialization;

namespace FwksLabs.Libs.Core.Configuration;

public static class JsonSerializerOptionsConfiguration
{
    public static JsonSerializerOptions Default()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        Configure(options);

        return options;
    }

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.WriteIndented = false;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.AllowTrailingCommas = true;

        options.Converters.Clear();

        options.Converters.Add(new JsonStringEnumConverter());
    }
}