using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Core.Contracts.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetAll;

public sealed class GetCustomersEndpoint : ICustomerEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Retrieve a list of orders")
        .Produces<PagedResponse<GetCustomersResponse>>();

    private async Task<IResult> HandleAsync(
        [AsParameters] GetCustomersRequest request,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var customers = await Task.FromResult(databaseContext.Customers.FindAll().ToList());

        return GetCustomersResponse.ToResponse(request, customers);
    }
}