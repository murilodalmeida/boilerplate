using System;

namespace FwksLabs.Libs.Infra.Redis.Options;

public sealed record FusionCacheRedisOptions
{
    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(30);
    public string? RedisConnectionString { get; set; }
    public bool EnableFailSafe { get; set; } = true;
    public TimeSpan JitterMaxDuration { get; set; } = TimeSpan.FromSeconds(30);
}