using System;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.Databases.LiteDb;
using FwksLabs.Libs.Infra.Databases.MongoDb;
using FwksLabs.Libs.Infra.Databases.Postgres;
using FwksLabs.Libs.Infra.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.HealthCheck.Options;
using Humanizer;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Npgsql;

namespace FwksLabs.Libs.Infra.HealthCheck.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddPostgresHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        return services
            .AddKeyedSingleton(nameof(PostgresHealthCheck), NpgsqlDataSource.Create(new NpgsqlConnectionStringBuilder(options.Target)))
            .AddHealthChecks()
            .AddDatabaseCheck<PostgresHealthCheck>(options.Name.IfEmpty("postgres"), options.FailureStatus, options.TimeoutInSeconds, GetTags(options))
            .Services;
    }

    public static IServiceCollection AddMongoDbHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        return services
            .AddKeyedSingleton<IMongoClient>(nameof(MongoDbHealthCheck), new MongoClient(options.Target))
            .AddHealthChecks()
            .AddDatabaseCheck<MongoDbHealthCheck>(options.Name.IfEmpty("mongodb"), options.FailureStatus, options.TimeoutInSeconds, GetTags(options))
            .Services;
    }

    public static IServiceCollection AddLiteDbHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        return services
            .AddKeyedSingleton<ILiteDatabase>(nameof(LiteDbHealthCheck), new LiteDatabase(options.Target))
            .AddHealthChecks()
            .AddDatabaseCheck<LiteDbHealthCheck>(options.Name.IfEmpty("litedb"), options.FailureStatus, options.TimeoutInSeconds, GetTags(options))
            .Services;
    }

    public static IServiceCollection AddHttpServiceHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        services
            .AddHttpClient($"{options.Name.Kebaberize()}-liveness", client =>
            {
                client.DefaultRequestHeaders.Add("X-Health-Check", options.Name);
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
                client.BaseAddress = new Uri($"{options.Target}/{options.LivenessPath.IfEmpty(AppProperties.HealthCheck.EndpointsLiveness).TrimStart('/')}");
            });

        return services
            .AddHealthChecks()
            .AddHttpServiceCheck<HttpServiceHealthCheck>(options.Name, options.FailureStatus, options.TimeoutInSeconds, GetTags(options))
            .Services;
    }

    private static string[] GetTags(HealthCheckDependencyOptions options)
    {
        string[] tags = [options.IsCritical ? AppProperties.HealthCheck.TagsCritical : AppProperties.HealthCheck.TagsNonCritical];

        if (options.Tags is not null)
            tags = [.. tags, .. options.Tags];

        return tags;
    }
}