using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.LiteDb.Configuration;
using FwksLabs.Boilerplate.Infra.MongoDb.Configuration;
using FwksLabs.Boilerplate.Infra.Postgres.Configuration;
using FwksLabs.Libs.Infra.Redis;
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
            .AddFusionCacheRedis(new() { RedisConnectionString = appSettings.Redis.ConnectionString });
    }
}