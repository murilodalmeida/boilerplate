using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.App.Api.Resources.Products.Requests;
using FwksLabs.Boilerplate.Core.Responses;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Endpoints;

public sealed class CreateProductEndpoint : AppResponses, IProductEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1, 0)
        .WithDescription("Creates a new product")
        .Produces<ResourceCreatedResponse>();

    public async Task<IResult> HandleAsync(
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
        IDatabaseContext databaseConntext,
        CancellationToken cancellationToken)
    {
        var result = await request.ValidateAsync(validator, cancellationToken);

        if (result.IsValid is false)
            return result.ToResponse();

        var product = request.ToProduct();

        await databaseConntext.Products.InsertOneAsync(product, null, cancellationToken);

        return Ok(ResourceCreatedResponse.Transform(product));
    }
}