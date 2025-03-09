using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ILiteContext = FwksLabs.Boilerplate.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Boilerplate.Infra.MongoDb.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Boilerplate.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.GetAll;

public sealed class GetPostsEndpoint : IPostEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet(string.Empty, HandleAsync)
        .MapToApiVersion(1)
        .Produces<GetPostsResponse>(StatusCodes.Status200OK);

    private async Task<IResult> HandleAsync(
        [FromServices] ILiteContext liteContext,
        [FromServices] IMongoContext mongoContext,
        [FromServices] IPostgresContext postgresContext,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        // lite
        var litedb = Transform([.. liteContext.Posts.FindAll()]);

        // mongo
        var mongo = Transform(await mongoContext.Posts.Find(_ => true).ToListAsync(cancellationToken));

        // postgres
        var postgres = Transform(await postgresContext.Posts.AsNoTracking().ToListAsync(cancellationToken));

        return GetPostsResponse.From(new()
        {
            { "litedb", litedb },
            { "mongo", mongo },
            { "postgres", postgres },
        });

        static IReadOnlyCollection<PostResponse> Transform(IReadOnlyCollection<PostEntity> posts) =>
            [.. posts.Select(PostResponse.From)];
    }
}