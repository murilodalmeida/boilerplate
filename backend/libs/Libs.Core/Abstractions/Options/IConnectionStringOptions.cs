using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Abstractions.Options;

public interface IConnectionStringOptions
{
    public string Server { get; set; }
    public Dictionary<string, object> Options { get; set; }
}