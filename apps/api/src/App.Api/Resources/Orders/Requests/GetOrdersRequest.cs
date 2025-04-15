using FwksLabs.Libs.Core.Contracts.Requests;

namespace FwksLabs.Boilerplate.App.Api.Resources.Orders.Requests;

public sealed record GetOrdersRequest(int PageNumber = 1, int PageSize = 10) : PageRequest(PageNumber, PageSize);