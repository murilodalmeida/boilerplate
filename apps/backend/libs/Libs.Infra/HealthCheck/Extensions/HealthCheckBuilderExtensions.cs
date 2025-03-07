using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.HealthCheck.Extensions;

public static class HealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddHttpServiceCheck<THealthCheck>(
        this IHealthChecksBuilder builder,
        string name,
        HealthStatus failureStatus = HealthStatus.Unhealthy,
        int timeoutInSeconds = AppProperties.HealthCheck.TimeoutInSeconds,
        IEnumerable<string>? tags = null) where THealthCheck : class, IHealthCheck =>
        builder
            .AddCheck<THealthCheck>(
                name,
                failureStatus,
                tags.With(AppProperties.HealthCheck.TagsTypeHttpService).Distinct(),
                TimeSpan.FromSeconds(timeoutInSeconds));

    public static IHealthChecksBuilder AddDatabaseCheck<THealthCheck>(
        this IHealthChecksBuilder builder,
        string? name = null,
        HealthStatus failureStatus = HealthStatus.Unhealthy,
        int timeoutInSeconds = AppProperties.HealthCheck.TimeoutInSeconds,
        IEnumerable<string>? tags = null) where THealthCheck : class, IHealthCheck =>
        builder
            .AddCheck<THealthCheck>(
                name.HasValue() ? name! : typeof(THealthCheck).Name.Replace("HealthCheck", string.Empty),
                failureStatus,
                tags.With(AppProperties.HealthCheck.TagsTypeDatabase).Distinct(),
                TimeSpan.FromSeconds(timeoutInSeconds));

    private static IEnumerable<string> With(this IEnumerable<string>? original, params string[] tags) =>
        [.. original is null ? [] : original, AppProperties.HealthCheck.TagsReadiness, .. tags];
}