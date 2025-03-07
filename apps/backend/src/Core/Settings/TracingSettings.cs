namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record TracingSettings
{
    public string[] IncomingRequestPathsFilter { get; set; } = [];
    public string[] OutgoingRequestPathsFilter { get; set; } = [];
}