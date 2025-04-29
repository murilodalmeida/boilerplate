using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions.Common;
using FwksLabs.Libs.Core.Contracts.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetCustomers;

public sealed class GetCustomersEndpoint : ICustomerEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .Produces<PagedResponse<GetCustomersResponse>>()
        .WithName("GetCustomers")
        .WithDescription("Get a list of customers");

    private async Task<IResult> HandleAsync(
        [AsParameters] GetCustomersRequest request,
        IValidator<GetCustomersRequest> validator,
        IQueryHandler<GetCustomersQuery, GetCustomersResult> handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
            return AppResponses.ValidationError(validationResult);

        var page = await handler.HandleAsync(request.ToQuery(), cancellationToken);

        return TypedResults.Ok(page.ToResponse(GetCustomersResponse.From));
    }
}