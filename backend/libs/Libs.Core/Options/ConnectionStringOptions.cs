using System.Collections.Generic;
using FwksLabs.Libs.Core.Abstractions.Options;

namespace FwksLabs.Libs.Core.Options;

public abstract record ConnectionStringOptions : IConnectionStringOptions
{
    public required string Server { get; set; }
    public Dictionary<string, object> Options { get; set; } = [];

    public abstract string BuildConnectionString();
}