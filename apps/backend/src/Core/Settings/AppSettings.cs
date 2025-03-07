using System.Collections.Generic;

namespace FwksLabs.Boilerplate.Core.Settings;

public sealed record AppSettings
{
    public required LiteDbSettings LiteDb { get; set; }
    public required LocalizationSettings Localization { get; set; }
    public required MongoDbSettings MongoDb { get; set; }
    public ObservabilitySettings Observability { get; set; } = new();
    public required PostgresSettings Postgres { get; set; }
    public SecuritySettings Security { get; set; } = new();
    public required ServiceInfoSettings ServiceInfo { get; set; }    
    public Dictionary<string, HttpServiceDependencySettings> Services { get; set; } = [];

    public bool IsDevelopment { get; set; }
}
