using System.Collections.Generic;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record LoggingSettings
{
    public bool WriteToConsole { get; set; } = false;
    public string MinimumLevel { get; set; } = string.Empty;
    public Dictionary<string, string> MinimumLevelOverrides { get; set; } = [];
}