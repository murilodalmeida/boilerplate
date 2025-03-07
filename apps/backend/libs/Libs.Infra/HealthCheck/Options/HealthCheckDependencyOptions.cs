using System.Collections.Generic;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.HealthCheck.Options;

public sealed record HealthCheckDependencyOptions
{
    public required string Name { get; set; }
    public string? Target { get; set; }
    public string? LivenessPath { get; set; }
    public HealthStatus FailureStatus { get; set; } = HealthStatus.Unhealthy;
    public bool IsCritical { get; set; }
    public int TimeoutInSeconds { get; set; } = AppProperties.HealthCheck.TimeoutInSeconds;
    public IEnumerable<string>? Tags { get; set; } = [];
}