using System;
using System.Linq;
using System.Web;
using FwksLabs.Libs.Core.Abstractions.Options;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static class ConnectionStringOptionsExtensions
{
    const string USR_KEY = "username";
    const string PSW_KEY = "password";
    const string DB_KEY = "database";

    public static string BuildConnectionString(
        this IConnectionStringOptions builder, char separator, char aggregator, string server, Func<string, string> format) =>
            string.Join(separator, [
                server,
                .. builder.Options.Select(o => $"{format(o.Key)}{aggregator}{o.Value}")
            ]);

    public static string BuildRedisConnectionString(this IConnectionStringOptions builder) =>
        builder.BuildConnectionString(',', '=', builder.Server, InflectorExtensions.Camelize);

    public static string BuildPostgresConnectionString(this IConnectionStringOptions builder) =>
        builder.BuildConnectionString(';', '=', $"Host={builder.Server}", InflectorExtensions.Pascalize);

    public static string BuildLiteDbConnectionString(this IConnectionStringOptions builder) =>
        builder.BuildConnectionString(';', '=', $"Filename={builder.Server}", InflectorExtensions.Pascalize);

    public static string BuildMongoDbConnectionString(this IConnectionStringOptions builder, bool encodeUrl = false)
    {
        var username = GetValue(USR_KEY);
        var password = GetValue(PSW_KEY);
        var database = GetValue(DB_KEY) ?? string.Empty;

        var credentials = username.IsEmpty() || password.IsEmpty() ? string.Empty : $"{username}:{password}@";

        var queryOptions = builder.Options
            .Where(o => new[] { USR_KEY, PSW_KEY, DB_KEY }.Contains(o.Key) is false)
            .Select(o => $"{o.Key.Camelize()}={o.Value}");

        var queryString = queryOptions.Any() ? $"?{string.Join('&', queryOptions)}" : string.Empty;

        var connectionString = $"mongodb://{credentials}{builder.Server}/{database}{queryString}";

        return encodeUrl ? HttpUtility.UrlEncode(connectionString) : connectionString;

        string? GetValue(string key) => builder.Options.TryGetValue(key, out var value) ? value?.ToString() : null;
    }

}