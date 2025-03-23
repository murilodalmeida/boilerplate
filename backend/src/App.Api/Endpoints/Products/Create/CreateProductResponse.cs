using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Products.Create;

public sealed record CreateProductResponse(string Id)
{
    internal static IResult ToResponse(ProductEntity product) =>
        AppResponses.Created(new CreateProductResponse(product.Id.Encode()));
}