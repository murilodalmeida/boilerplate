namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record CorsPolicySettings
{
    public string[] AllowedHeaders { get; set; } = [];
    public string[] AllowedMethods { get; set; } = [];
    public string[] AllowedOrigins { get; set; } = [];
    public string[] ExposedHeaders { get; set; } = [];
}