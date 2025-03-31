using System;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.Redis.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace FwksLabs.Libs.Infra.Redis;

public static class RedisConfiguration
{
    public static IServiceCollection AddRedisHybridCache(this IServiceCollection services, HybridCacheOptions options)
    {
        if (options.RedisConnectionString is null)
            throw new ArgumentNullException(nameof(options), $"{nameof(options.RedisConnectionString)} is not a valid redis connection string. Check the values and try again.");

        services
            .AddMemoryCache()
            .AddFusionCache()
            .WithDefaultEntryOptions(BuildDefaultOptions())
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .WithMemoryCache(sp => sp.GetRequiredService<IMemoryCache>())
            .AsHybridCache()
            .WithDistributedCache(BuildRedisCache());

        return services;

        Action<FusionCacheEntryOptions> BuildDefaultOptions() =>
            defaultOptions =>
            {
                defaultOptions.Duration = TimeSpan.FromMilliseconds(options.DurationMs);
                defaultOptions.IsFailSafeEnabled = options.IsFailSafeEnabled;
                defaultOptions.FailSafeThrottleDuration = TimeSpan.FromMilliseconds(options.FailSafeThrottleDurationMs);
                defaultOptions.DistributedCacheSoftTimeout = TimeSpan.FromMilliseconds(options.DistributedCacheSoftTimeoutMs);
                defaultOptions.DistributedCacheHardTimeout = TimeSpan.FromMilliseconds(options.DistributedCacheHardTimeoutMs);
            };

        RedisCache BuildRedisCache() =>
            new(new RedisCacheOptions { Configuration = options.RedisConnectionString.BuildRedisConnectionString() });
    }
}