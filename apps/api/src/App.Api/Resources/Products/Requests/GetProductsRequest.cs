using FwksLabs.Libs.Core.Contracts.Requests;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Requests;

public sealed record GetProductsRequest(int PageNumber = 1, int PageSize = 10) : PageRequest(PageNumber, PageSize);