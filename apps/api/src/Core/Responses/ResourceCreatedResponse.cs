using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Boilerplate.Core.Responses;

public sealed record ResourceCreatedResponse(string Id)
{
    public static ResourceCreatedResponse Transform(CustomerEntity entity) => new(entity.EncodeId());
    public static ResourceCreatedResponse Transform(ProductEntity entity) => new(entity.EncodeId());
    public static ResourceCreatedResponse Transform(OrderEntity entity) => new(entity.EncodeId());
}