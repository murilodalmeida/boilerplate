using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;

public sealed record GetCustomersQuery(int PageNumber, int PageSize) : PageQuery(PageNumber, PageSize);