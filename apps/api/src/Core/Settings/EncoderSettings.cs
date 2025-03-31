namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record EncoderSettings
{
    public required string Base62Alphabet { get; set; }
}