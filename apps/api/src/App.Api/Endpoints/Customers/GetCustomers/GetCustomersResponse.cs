using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetCustomers;

public sealed record GetCustomersResponse(string Id, string Name, string? PhoneNumber, string? Email)
{
    public static Func<IEnumerable<GetCustomersResult>, IReadOnlyCollection<GetCustomersResponse>> From =>
        items => [.. items.Select(x => new GetCustomersResponse(x.Id.Encode(), x.Name, x.PhoneNumber, x.Email))];
}