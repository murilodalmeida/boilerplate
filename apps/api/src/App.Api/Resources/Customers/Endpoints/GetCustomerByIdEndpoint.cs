using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.App.Api.Resources.Customers.Responses;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Resources.Customers.Endpoints;

public sealed class GetCustomerByIdEndpoint : AppResponses, ICustomerEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet("{id}", HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Retrieve a customer by id")
        .Produces<CustomerResponse>();

    private async Task<IResult> HandleAsync(
        string id,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var customer = await Task.FromResult(databaseContext.Customers.FindById(id.Decode()));

        if (customer is null)
            return NotFound();

        return Ok(CustomerResponse.Transform(customer));
    }
}