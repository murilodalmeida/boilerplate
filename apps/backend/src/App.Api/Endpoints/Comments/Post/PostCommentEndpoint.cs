using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ILiteContext = FwksLabs.Boilerplate.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Boilerplate.Infra.MongoDb.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Boilerplate.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Post;

internal sealed class PostCommentEndpoint : ICommentEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .MapToApiVersion(1)
        .Produces<PostCommentResponse>(StatusCodes.Status201Created);

    private async Task<IResult> HandleAsync(
        [FromServices] ILiteContext liteContext,
        [FromServices] IMongoContext mongoContext,
        [FromServices] IPostgresContext postgresContext,
        [FromServices] IValidator<PostCommentRequest> validator,
        [FromBody] PostCommentRequest request,
        CancellationToken cancellationToken)
    {
        var comment = request.ToEntity();

        // mongo
        await mongoContext.Comments.InsertOneAsync(comment, null, cancellationToken);

        // postgres
        await postgresContext.Comments.AddAsync(comment, cancellationToken);
        await postgresContext.SaveChangesAsync(cancellationToken);

        // lite
        liteContext.Comments.Insert(comment);

        return PostCommentResponse.From(comment);
    }
}