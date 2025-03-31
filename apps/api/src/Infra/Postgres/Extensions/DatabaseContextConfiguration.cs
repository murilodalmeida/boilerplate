using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra.Postgres.Extensions;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddEntityFrameworkPostgres(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<IDatabaseContext, DatabaseContext>(x => x.UseNpgsql(appSettings.Postgres.BuildPostgresConnectionString()));

        return services;
    }
}