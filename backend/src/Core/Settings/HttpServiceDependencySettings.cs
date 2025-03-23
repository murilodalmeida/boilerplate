namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record HttpServiceDependencySettings(
    string Url,
    bool Critical = true,
    string? LivenessPath = null,
    int TimeoutInSeconds = 5,
    bool IsExternal = false);