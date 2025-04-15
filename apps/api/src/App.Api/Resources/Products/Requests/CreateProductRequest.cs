using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Requests;

public sealed record CreateProductRequest(string Name, string Description, decimal Price) : IRequest
{
    public ProductEntity ToProduct() => new()
    {
        Name = Name,
        Description = Description,
        Price = Price
    };
}