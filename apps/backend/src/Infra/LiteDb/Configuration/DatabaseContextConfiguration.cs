using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Abstractions;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.LiteDb.Abstractions;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra.LiteDb.Configuration;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddLiteDb(this IServiceCollection services, AppSettings appSettings)
    {
        typeof(IInfra).ConfigureFromType<ITypeConfiguration>();

        return services
            .AddSingleton(new LiteDatabase(appSettings.LiteDb.ConnectionString))
            .AddSingleton<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<LiteDatabase>()));
    }
}