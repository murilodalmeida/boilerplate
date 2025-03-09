using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Post;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ILiteContext = FwksLabs.Boilerplate.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Boilerplate.Infra.MongoDb.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Boilerplate.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Delete;

internal sealed class DeleteCommentEndpoint : ICommentEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapDelete("{id}", HandleAsync)
        .MapToApiVersion(1)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private async Task<IResult> HandleAsync(
        [FromServices] ILiteContext liteContext,
        [FromServices] IMongoContext mongoContext,
        [FromServices] IPostgresContext postgresContext,
        [FromServices] IValidator<PostCommentRequest> validator,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // mongo
        var filter = Builders<CommentEntity>.Filter.Eq(x => x.Id, id);
        await mongoContext.Comments.DeleteOneAsync(filter, cancellationToken);

        // postgres
        var comment = await postgresContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        if (comment is not null)
        {
            postgresContext.Comments.Remove(comment);
            await postgresContext.SaveChangesAsync(cancellationToken);
        }

        // lite
        liteContext.Comments.Delete(id);

        return AppResponses.NoContent();
    }
}