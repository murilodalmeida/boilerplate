using FwksLabs.Libs.Core.Abstractions.Options;

namespace FwksLabs.Libs.Infra.Redis.Options;

public sealed record HybridCacheOptions
{
    public int DurationMs { get; set; } = 30 * 1000;
    public bool IsFailSafeEnabled { get; set; } = true;
    public int FailSafeThrottleDurationMs { get; set; } = 60 * 1000;
    public int DistributedCacheSoftTimeoutMs { get; set; } = 3 * 1000;
    public int DistributedCacheHardTimeoutMs { get; set; } = 5 * 1000;
    public IConnectionStringOptions? RedisConnectionString { get; set; }
}