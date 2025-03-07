using FwksLabs.Boilerplate.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Infra;

public static class InfraConfiguration
{
    public static IServiceCollection AddInfraDependencies(this IServiceCollection services, AppSettings appSettings)
    {
        return services;
    }
}
