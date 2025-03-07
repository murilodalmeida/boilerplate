using FwksLabs.Libs.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class CorrelationIdMiddlewareConfiguration
{
    public static IApplicationBuilder UseCorrelationId(this WebApplication app) =>
        app.UseMiddleware<CorrelationIdMiddleware>();
}