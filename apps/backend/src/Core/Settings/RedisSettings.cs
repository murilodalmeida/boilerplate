using FwksLabs.Libs.Core.Abstractions.Options;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record RedisSettings : IConnectionStringOptions
{
    public required string ConnectionString { get; set; }
}