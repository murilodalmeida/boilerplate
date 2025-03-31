using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Infra.Postgres.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.GetAll;

public sealed class GetOrdersEndpoint : IOrderEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder.MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Get all orders")
        .Produces<PagedResult<GetOrdersResponse>>();

    private async Task<IResult> HandleAsync(
        [AsParameters] GetOrdersRequest request,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var ordersPage = await databaseContext.Orders.GetPageAsync(request, cancellationToken);

        return GetOrdersResponse.ToResponse(ordersPage);
    }
}