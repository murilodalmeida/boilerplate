using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Responses;

public sealed record GetProductsResponse(string Id, string Name, string Description, decimal Price)
{
    internal static PagedResponse<GetProductsResponse> Transform(PagedResult<ProductEntity> productsPage)
    {
        var items = productsPage.Items.Select(x => new GetProductsResponse(x.Id.Encode(), x.Name, x.Description, x.Price)).ToList();

        return new(productsPage.PageNumber, productsPage.PageSize, items.Count, items);
    }
}