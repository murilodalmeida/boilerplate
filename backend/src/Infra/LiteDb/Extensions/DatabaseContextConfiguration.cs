using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Abstractions;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.LiteDb.Abstractions;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra.LiteDb.Extensions;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddLiteDb(this IServiceCollection services, AppSettings appSettings)
    {
        typeof(IInfra).ConfigureFromType<ITypeConfiguration>();

        services
            .AddSingleton<ILiteDatabase>(new LiteDatabase(appSettings.LiteDb.BuildLiteDbConnectionString()))
            .AddSingleton<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<ILiteDatabase>()));

        return services;
    }
}