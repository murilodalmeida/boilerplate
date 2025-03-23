using System.Collections.Generic;

namespace FwksLabs.Libs.AspNetCore.Options;

public sealed record LoggingConfigurationOptions
{
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceNamespace { get; set; } = string.Empty;
    public string ServiceTeam { get; set; } = string.Empty;
    public string CollectorEndpoint { get; set; } = string.Empty;
    public string CollectorEndpointProtocol { get; set; } = string.Empty;
    public LoggingOptions Logging { get; set; } = new();
    public Dictionary<string, object> Attributes { get; set; } = [];
}