using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.Create;

public sealed class CreateOrderEndpoint : IOrderEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder.MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Get all orders")
        .Produces<CreateOrderResponse>();

    private async Task<IResult> HandleAsync(
        CreateOrderRequest request,
        IValidator<CreateOrderRequest> validator,
        IDatabaseContext databaseContext,
        CancellationToken cancellationToken)
    {
        var validation = await request.ValidateAsync(validator, cancellationToken);

        if (validation.IsValid is false)
            return AppResponses.ValidationErrors(validation);

        var order = request.ToOrder();

        await databaseContext.Orders.AddAsync(order, cancellationToken);

        await databaseContext.SaveChangesAsync(cancellationToken);

        return CreateOrderResponse.ToResponse(order);
    }
}