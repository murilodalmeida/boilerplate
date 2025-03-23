using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Core.ValueObject;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;
using FwksLabs.Libs.Core.Security.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.GetAll;

public sealed record GetOrdersResponse(
    string Id, string CustomerId, string CreationDate, string? PaymentDate, ICollection<OrderProductValueObject> Products, decimal Total)
{
    public static IResult ToResponse(PagedResult<OrderEntity> page)
    {
        var items = page.Items.Select(x =>
            new GetOrdersResponse(
                x.Id.Encode(), x.CustomerId.Encode(), x.CreationDate.Humanize(), x.PaymentDate.Humanize(), x.Products, x.Total)).ToList();

        return AppResponses.Ok(
            Page<GetOrdersResponse>.From(page.PageNumber, page.PageSize, items.Count, items));
    }
}