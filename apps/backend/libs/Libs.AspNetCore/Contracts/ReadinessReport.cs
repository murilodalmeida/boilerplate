using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.Contracts;

public sealed record ReadinessReport
{
    public static ReadinessReport From(HealthReport report)
    {
        return new()
        {
            Status = GetStatus(report),
            TotalDuration = report.TotalDuration,
            Dependencies = [.. report.Entries.Select(ReadinessDependencyReport.From)]
        };
    }

    public required HealthStatus Status { get; init; }
    public TimeSpan TotalDuration { get; init; }
    public IReadOnlyCollection<ReadinessDependencyReport> Dependencies { get; init; } = [];

    static HealthStatus GetStatus(HealthReport report)
    {
        var status = report.Status;

        if (status == HealthStatus.Unhealthy)
            status = HasCriticalUnhealthy() ? HealthStatus.Unhealthy : HealthStatus.Degraded;

        return status;

        bool HasCriticalUnhealthy() =>
            report.Entries.Any(x => x.Value.Tags.Contains(AppProperties.HealthCheck.TagsCritical) && x.Value.Status is not HealthStatus.Healthy);

    }
}