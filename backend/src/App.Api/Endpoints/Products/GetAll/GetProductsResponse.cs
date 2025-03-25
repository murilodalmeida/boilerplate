using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Products.GetAll;

public sealed record GetProductsResponse(string Id, string Name, string Description, decimal Price)
{
    internal static IResult ToResponse(PagedResult<ProductEntity> productsPage)
    {
        var items = productsPage.Items.Select(x => new GetProductsResponse(x.Id.Encode(), x.Name, x.Description, x.Price)).ToList();

        return AppResponses.Ok(
            PagedResponse<GetProductsResponse>.From(
                productsPage.PageNumber, productsPage.PageSize, items.Count, items));
    }
}