using FwksLabs.Boilerplate.Application.Abstractions;
using FwksLabs.Libs.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.Application.Features;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        return services
            .AddHandlersFromAssembly<IApplication>();
    }
}
