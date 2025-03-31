using System.Collections.Generic;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record ObservabilitySettings
{
    public string CollectorEndpoint { get; set; } = string.Empty;
    public string CollectorEndpointProtocol { get; set; } = string.Empty;
    public LoggingSettings Logging { get; set; } = new();
    public Dictionary<string, object> Attributes { get; set; } = [];
    public TracingSettings Tracing { get; set; } = new();
    public MetricsSettings Metrics { get; set; } = new();
}