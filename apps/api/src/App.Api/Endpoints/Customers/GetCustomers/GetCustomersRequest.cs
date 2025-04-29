using FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;
using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetCustomers;

public sealed record GetCustomersRequest(int PageNumber = 1, int PageSize = 10) : PageRequest(PageNumber, PageSize)
{
    public GetCustomersQuery ToQuery() => new(PageNumber, PageSize);
}