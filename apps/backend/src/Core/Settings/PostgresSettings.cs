using FwksLabs.Libs.Core.Abstractions.Options;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record PostgresSettings : IConnectionStringOptions
{
    public required string ConnectionString { get; set; }
}