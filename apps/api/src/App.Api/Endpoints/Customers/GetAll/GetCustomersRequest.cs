using FwksLabs.Libs.Core.Contracts.Requests;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetAll;

public sealed record GetCustomersRequest(int PageNumber = 1, int PageSize = 10) : PageRequest(PageNumber, PageSize);