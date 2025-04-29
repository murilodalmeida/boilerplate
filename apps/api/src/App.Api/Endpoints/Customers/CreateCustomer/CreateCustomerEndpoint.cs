using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.CreateCustomer;

public sealed class CreateCustomerEndpoint : ICustomerEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .Produces<CreateCustomerResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("CreateCustomer")
        .WithDescription("Creates a customer");

    private async Task<IResult> HandleAsync(CreateCustomerRequest request,
        IValidator<CreateCustomerRequest> validator,
        ICommandHandler<CreateCustomerCommand, CreateCustomerResult> handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
            return AppResponses.ValidationError(validationResult);

        var result = await handler.HandleAsync(request.ToCommand(), cancellationToken);

        if (result.IsValid is false)
            return AppResponses.ValidationError(result.ValidationResult!);

        return TypedResults.Ok(CreateCustomerResponse.From(result.Data!));
    }
}