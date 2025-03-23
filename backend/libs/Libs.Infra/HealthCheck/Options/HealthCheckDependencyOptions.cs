using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.HealthCheck.Options;

public sealed record HealthCheckDependencyOptions(string Name, string Target)
{
    public string? LivenessPath { get; set; }
    public HealthStatus FailureStatus { get; set; } = HealthStatus.Unhealthy;
    public bool IsCritical { get; set; } = true;
    public int TimeoutInSeconds { get; set; } = AppProperties.HealthCheck.TimeoutInSeconds;
    public IEnumerable<string>? Tags { get; set; } = [];

    public TimeSpan GetTimeout() => TimeSpan.FromSeconds(TimeoutInSeconds);

    public void AddTags(params string[] tags)
    {
        if (Tags is null || tags.Length == 0)
            return;

        Tags = Tags.Concat(tags).Distinct();
    }

    public Uri GetLivenessUrl() =>
        new($"{Target}/{LivenessPath.WhenEmpty(AppProperties.HealthCheck.EndpointsLiveness).TrimStart('/')}");
}