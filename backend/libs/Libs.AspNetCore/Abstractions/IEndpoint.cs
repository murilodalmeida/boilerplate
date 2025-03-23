using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.Abstractions;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder builder);
}