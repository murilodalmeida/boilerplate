using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.Create;

public sealed record CreateOrderResponse(string Id, decimal Total)
{
    internal static IResult ToResponse(OrderEntity order) =>
        AppResponses.Ok(new CreateOrderResponse(order.Id.Encode(), order.Total));
}