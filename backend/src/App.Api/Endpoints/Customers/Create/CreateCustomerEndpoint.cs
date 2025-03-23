using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.Create;

public sealed class CreateCustomerEndpoint : ICustomerEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Creates a new customer")
        .Produces<CreateCustomerResponse>();

    private async Task<IResult> HandleAsync(
        CreateCustomerRequest request,
        IValidator<CreateCustomerRequest> validator,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (validation.IsValid is false)
            return AppResponses.ValidationErrors(validation);

        var customer = request.ToCustomer();

        databaseContext.Customers.Insert(customer);

        return CreateCustomerResponse.ToResponse(customer);
    }
}