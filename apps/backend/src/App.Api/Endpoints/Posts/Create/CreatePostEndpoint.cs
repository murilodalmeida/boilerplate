using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ILiteContext = FwksLabs.Boilerplate.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Boilerplate.Infra.MongoDb.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Boilerplate.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.Create;

public sealed class CreatePostEndpoint : IPostEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1)
        .Produces<CreatePostResponse>(StatusCodes.Status201Created);

    private async Task<IResult> HandleAsync(
        [FromServices] ILiteContext liteContext,
        [FromServices] IMongoContext mongoContext,
        [FromServices] IPostgresContext postgresContext,
        [FromServices] IValidator<CreatePostRequest> validator,
        [FromBody] CreatePostRequest request,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
            return AppResponses.ValidationErrors(validationResult);

        var post = request.ToEntity();

        // mongo
        await mongoContext.Posts.InsertOneAsync(post, null, cancellationToken);

        // postgres
        await postgresContext.Posts.AddAsync(post, cancellationToken);
        await postgresContext.SaveChangesAsync(cancellationToken);

        // lite
        liteContext.Posts.Insert(post);

        return CreatePostResponse.From(post);
    }
}