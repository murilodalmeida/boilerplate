using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Resources.Orders.Responses;

public sealed record CreateOrderResponse(string Id, decimal Total)
{
    internal static CreateOrderResponse Transform(OrderEntity order) =>
        new(order.Id.Encode(), order.Total);
}