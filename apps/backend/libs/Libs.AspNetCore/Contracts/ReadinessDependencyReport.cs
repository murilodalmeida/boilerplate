using System;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.Contracts;

public sealed record ReadinessDependencyReport
{
    public required string Name { get; init; }
    public required TimeSpan Duration { get; init; }
    public required HealthStatus Status { get; init; }
    public required string[] Tags { get; init; }

    public static ReadinessDependencyReport From(KeyValuePair<string, HealthReportEntry> entry)
    {
        return new()
        {
            Name = entry.Key,
            Duration = entry.Value.Duration,
            Status = entry.Value.Status,
            Tags = [.. entry.Value.Tags]
        };
    }
}