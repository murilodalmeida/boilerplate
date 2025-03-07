using FwksLabs.Boilerplate.App.Api.Configuration;
using FwksLabs.Boilerplate.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Scalar.AspNetCore;

namespace FwksLabs.Boilerplate.App.Api.Configuration;

public static class OpenApiConfiguration
{
    public static void MapOpenApi(this IEndpointRouteBuilder builder, AppSettings appSettings)
    {
        builder.MapOpenApi();
        builder.MapScalarApiReference();
    }
}
