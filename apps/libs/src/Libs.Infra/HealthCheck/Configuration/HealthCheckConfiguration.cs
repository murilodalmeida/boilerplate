using System;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.HealthCheck.Options;
using FwksLabs.Libs.Infra.LiteDb.HealthCheck;
using FwksLabs.Libs.Infra.MongoDb.Contexts;
using FwksLabs.Libs.Infra.MongoDb.HealthCheck;
using FwksLabs.Libs.Infra.Postgres.HealthCheck;
using FwksLabs.Libs.Infra.Redis.HealthCheck;
using Humanizer;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
using StackExchange.Redis;

namespace FwksLabs.Libs.Infra.HealthCheck.Configuration;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddCheck<TService, THealthCheck>(
        this IServiceCollection services, string? serviceName, Func<IServiceProvider, object, TService> implementation, HealthCheckDependencyOptions options, string healthCheckFallbackName)
        where TService : class
        where THealthCheck : class, IHealthCheck
    {
        var failureStatus = options.IsCritical ? HealthStatus.Unhealthy : HealthStatus.Degraded;

        options.AddTags(
            AppProperties.HealthCheck.TagsReadiness,
            options.IsCritical ? AppProperties.HealthCheck.TagsCritical : AppProperties.HealthCheck.TagsNonCritical
        );

        return services
            .AddKeyedTransient(serviceName.WhenEmpty(typeof(THealthCheck).Name), implementation)
            .AddHealthChecks()
            .AddCheck<THealthCheck>(options.Name.WhenEmpty(healthCheckFallbackName), failureStatus, options.Tags, options.GetTimeout())
            .Services;
    }

    public static IServiceCollection AddPostgresHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        options.AddTags(AppProperties.HealthCheck.TagsTypePostgres);

        return services.AddCheck<NpgsqlDataSource, PostgresHealthCheck>(null, (_, _) => NpgsqlDataSource.Create(options.Target), options, "postgres");
    }

    public static IServiceCollection AddMongoDbHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options, bool isAdmin = false)
    {
        options.AddTags(AppProperties.HealthCheck.TagsTypeMongoDb);

        return services.AddCheck<MongoDbHealthCheckContext, MongoDbHealthCheck>(null, (_, _) => new MongoDbHealthCheckContext(options.Target) { IsAdmin = isAdmin }, options, "mongodb");
    }

    public static IServiceCollection AddLiteDbHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        options.AddTags(AppProperties.HealthCheck.TagsTypeLiteDb);

        return services.AddCheck<ILiteDatabase, LiteDbHealthCheck>(null, (_, _) => new LiteDatabase(options.Target), options, "litedb");
    }

    public static IServiceCollection AddRedisHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        options.AddTags(AppProperties.HealthCheck.TagsTypeRedis);

        return services.AddCheck<IConnectionMultiplexer, RedisHealthCheck>(null, (_, _) => ConnectionMultiplexer.Connect(options.Target), options, "redis");
    }

    public static IServiceCollection AddHttpServiceHealthChecks(this IServiceCollection services, params HealthCheckDependencyOptions[] servicesOptions)
    {
        foreach (var options in servicesOptions)
            services.AddHttpServiceHealthCheck(options);

        return services;
    }

    public static IServiceCollection AddHttpServiceHealthCheck(this IServiceCollection services, HealthCheckDependencyOptions options)
    {
        var name = $"{options.Name.Kebaberize()}-liveness";

        options.AddTags(
            AppProperties.HealthCheck.TagsTypeHttpService,
            AppProperties.HealthCheck.TagsReadiness,
            options.IsCritical ? AppProperties.HealthCheck.TagsCritical : AppProperties.HealthCheck.TagsNonCritical);

        services.AddHttpClient(name, client =>
        {
            client.DefaultRequestHeaders.Add(AppProperties.Headers.HealthCheckClient, options.Name);
            client.BaseAddress = options.GetLivenessUrl();
            client.Timeout = options.GetTimeout();
        });

        services
            .AddHealthChecks()
            .AddCheck<HttpServiceHealthCheck>(
                name,
                options.IsCritical ? HealthStatus.Unhealthy : HealthStatus.Degraded,
                options.Tags,
                options.GetTimeout());

        return services;
    }
}