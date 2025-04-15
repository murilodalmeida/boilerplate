using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.App.Api.Resources.Products.Requests;
using FwksLabs.Boilerplate.App.Api.Resources.Products.Responses;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Infra.MongoDb.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Endpoints;

public sealed class GetProductsEndpoint : AppResponses, IProductEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Retrieve a list of products")
        .Produces<PagedResponse<GetProductsResponse>>();

    public async Task<IResult> HandleAsync(
    [AsParameters] GetProductsRequest request,
    IDatabaseContext databaseConntext,
    CancellationToken cancellationToken)
    {
        var productsPage = await databaseConntext.Products.GetPageAsync(request, cancellationToken);

        return Ok(GetProductsResponse.Transform(productsPage));
    }
}