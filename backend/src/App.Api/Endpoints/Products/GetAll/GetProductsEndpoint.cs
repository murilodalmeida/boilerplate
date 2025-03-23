using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.Infra.MongoDb.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Products.GetAll;

public sealed class GetProductsEndpoint : IProductEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Retrieve a list of products")
        .Produces<GetProductsResponse>();

    public async Task<IResult> HandleAsync(
    [AsParameters] GetProductsRequest request,
    IDatabaseContext databaseConntext,
    CancellationToken cancellationToken)
    {
        var productsPage = await databaseConntext.Products.GetPageAsync(request, cancellationToken);

        return GetProductsResponse.ToResponse(productsPage);
    }
}