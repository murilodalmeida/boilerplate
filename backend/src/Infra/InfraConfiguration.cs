using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.LiteDb.Extensions;
using FwksLabs.Boilerplate.Infra.MongoDb.Extensions;
using FwksLabs.Boilerplate.Infra.Postgres.Extensions;
using FwksLabs.Libs.Infra.Redis;
using FwksLabs.Libs.Infra.Redis.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra;

public static class InfraConfiguration
{
    public static IServiceCollection AddInfraDependencies(this IServiceCollection services, AppSettings appSettings)
    {
        return services
            .AddEntityFrameworkPostgres(appSettings)
            .AddMongoDb(appSettings)
            .AddLiteDb(appSettings)
            .AddRedisHybridCache(BuildRedisOptions());

        HybridCacheOptions BuildRedisOptions() =>
            new() { RedisConnectionString = appSettings.Redis };
    }
}