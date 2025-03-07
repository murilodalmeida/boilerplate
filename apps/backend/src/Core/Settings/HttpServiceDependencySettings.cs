namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record HttpServiceDependencySettings(string Url, bool Critical, string? LivenessPath = null, int TimeoutInSeconds = 5);