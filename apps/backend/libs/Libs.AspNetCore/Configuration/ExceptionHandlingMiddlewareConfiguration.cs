using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class ExceptionHandlingMiddlewareConfiguration
{
    public static IServiceCollection AddExceptionHandlerService(this IServiceCollection services) =>
        services.AddScoped<IExceptionHandlerService, ExceptionHandlerService>();

    public static IServiceCollection AddExceptionHandlerService<TService>(this IServiceCollection services)
        where TService : class, IExceptionHandlerService =>
            services.AddScoped<IExceptionHandlerService, TService>();

    public static IApplicationBuilder UseExceptionHandlerService(this IApplicationBuilder builder) =>
        builder.UseExceptionHandler(handler => handler.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();

            if (feature is null)
                return;

            await context.RequestServices
                .GetRequiredService<IExceptionHandlerService>()
                .HandleAsync(context, feature.Error);
        }));
}