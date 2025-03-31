using System.Collections.Generic;

namespace FwksLabs.Libs.AspNetCore.Options;

public sealed record LoggingOptions
{
    public string MinimumLevel { get; set; } = string.Empty;
    public Dictionary<string, string> MinimumLevelOverrides { get; set; } = [];
}