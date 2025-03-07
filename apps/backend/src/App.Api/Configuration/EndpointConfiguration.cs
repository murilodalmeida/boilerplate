using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Libs.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;

namespace FwksLabs.Boilerplate.App.Api.Configuration;

internal static class EndpointConfiguration
{
    internal static void MapEndpoints(this WebApplication app)
    {
        app.MapEndpoints<IPostEndpoint>("posts");
        app.MapEndpoints<ICommentEndpoint>("comments");
    }
}