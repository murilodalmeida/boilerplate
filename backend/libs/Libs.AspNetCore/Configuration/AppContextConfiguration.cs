using FwksLabs.Libs.AspNetCore.Contracts;
using FwksLabs.Libs.AspNetCore.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class AppContextConfiguration
{
    public static IServiceCollection AddAppContext(this IServiceCollection services) =>
        services
            .AddScoped<AppRequestContext>()
            .AddScoped<CorrelationIdMiddleware>();
}