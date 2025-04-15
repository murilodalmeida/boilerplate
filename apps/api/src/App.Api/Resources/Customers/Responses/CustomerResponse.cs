using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.App.Api.Resources.Customers.Requests;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Resources.Customers.Responses;

public sealed record CustomerResponse(string Id, string Name, string? PhoneNumber, string? Email)
{
    public static PagedResponse<CustomerResponse> Transform(GetCustomersRequest request, IEnumerable<CustomerEntity> customers)
    {
        var items = customers.Select(Transform).ToList();

        return new(request.PageNumber, request.PageSize, items.Count, items);
    }

    public static CustomerResponse Transform(CustomerEntity customer) =>
        new(customer.Id.Encode(), customer.Name, customer.PhoneNumber, customer.Email);
}