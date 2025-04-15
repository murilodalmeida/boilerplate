using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.App.Api.Resources.Customers.Requests;
using FwksLabs.Boilerplate.App.Api.Resources.Customers.Responses;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Contracts.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Resources.Customers.Endpoints;

public sealed class GetCustomersEndpoint : AppResponses, IProductEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Retrieve a list of orders")
        .Produces<PagedResponse<CustomerResponse>>();

    private async Task<IResult> HandleAsync(
        [AsParameters] GetCustomersRequest request,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var customers = await Task.FromResult(databaseContext.Customers.FindAll().ToList());

        return Ok(CustomerResponse.Transform(request, customers));
    }
}