using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Core.ValueObject;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Resources.Orders.Requests;

public sealed record CreateOrderRequest(string CustomerId, IReadOnlyCollection<OrderProductRequest> Products) : IRequest
{
    public OrderEntity ToOrder() => new()
    {
        CustomerId = CustomerId.Decode(),
        Products = [.. Products.Select(x => new OrderProductValueObject(x.ProductId.Decode(), x.Quantity, x.Total))]
    };
}