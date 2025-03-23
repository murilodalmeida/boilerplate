using System.Collections.Generic;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record SecuritySettings
{
    public Dictionary<string, CorsPolicySettings> CorsPolicies { get; set; } = [];
    public required EncoderSettings Encoders { get; set; }
}