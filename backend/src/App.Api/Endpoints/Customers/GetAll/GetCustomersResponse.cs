using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetAll;

public sealed record GetCustomersResponse(string Id, string Name, string? PhoneNumber, string? Email)
{
    public static IResult ToResponse(GetCustomersRequest request, IEnumerable<CustomerEntity> customers)
    {
        var items = customers.Select(x => new GetCustomersResponse(x.Id.Encode(), x.Name, x.PhoneNumber, x.Email)).ToList();

        return AppResponses.Ok(
            PagedResponse<GetCustomersResponse>.From(
                request.PageNumber, request.PageSize, items.Count, items));
    }
}