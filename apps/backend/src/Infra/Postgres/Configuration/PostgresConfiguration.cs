using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra.Postgres.Configuration;

public static class PostgresConfiguration
{
    public static IServiceCollection AddEntityFrameworkPostgres(this IServiceCollection services, AppSettings appSettings) =>
        services.AddDbContext<IDatabaseContext, DatabaseContext>(x => x.UseNpgsql(appSettings.Postgres.ConnectionString));
}
