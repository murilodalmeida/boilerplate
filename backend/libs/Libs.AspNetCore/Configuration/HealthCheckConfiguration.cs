using System.Collections.Generic;
using FwksLabs.Libs.AspNetCore.Contracts;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class HealthCheckConfiguration
{
    public static IApplicationBuilder UseHealthCheckEndpoints(this IApplicationBuilder builder)
    {
        Dictionary<HealthStatus, int> statusCodeMapper = new()
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
        };

        return builder
            .UseHealthChecks(AppProperties.HealthCheck.EndpointsLiveness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppProperties.HealthCheck.TagsLiveness),
                AllowCachingResponses = false,
                ResultStatusCodes = statusCodeMapper,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(new { report.Status })
            })
            .UseHealthChecks(AppProperties.HealthCheck.EndpointsReadiness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppProperties.HealthCheck.TagsReadiness),
                AllowCachingResponses = false,
                ResultStatusCodes = statusCodeMapper,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(ReadinessReport.From(report))
            });
    }
}