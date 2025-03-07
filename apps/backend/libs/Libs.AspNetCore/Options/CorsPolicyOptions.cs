namespace FwksLabs.Libs.AspNetCore.Options;

public sealed record CorsPolicyOptions(
    string Name,
    string[] AllowedHeaders,
    string[] AllowedMethods,
    string[] AllowedOrigins,
    string[] ExposedHeaders);