using FwksLabs.Libs.Infra.Redis.Options;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace FwksLabs.Libs.Infra.Redis;

public static class FusionCacheRedisConfiguration
{
    public static IServiceCollection AddFusionCacheRedis(this IServiceCollection services, FusionCacheRedisOptions options)
    {
        var builder = services
            .AddFusionCache()
            .WithDefaultEntryOptions(o =>
            {
                o.Duration = options.Duration;
                o.IsFailSafeEnabled = options.EnableFailSafe;
                o.JitterMaxDuration = options.JitterMaxDuration;
            })
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .AsHybridCache();

        if (options.RedisConnectionString is not null)
            builder.WithDistributedCache(BuildRedis());

        RedisCache BuildRedis() => new(new RedisCacheOptions
        {
            Configuration = options.RedisConnectionString
        });

        return services;
    }
}