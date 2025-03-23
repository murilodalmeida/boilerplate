using FwksLabs.Libs.Core.Contracts.Requests;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Products.GetAll;

public sealed record GetProductsRequest(int PageNumber = 1, int PageSize = 10) : PageRequest(PageNumber, PageSize);