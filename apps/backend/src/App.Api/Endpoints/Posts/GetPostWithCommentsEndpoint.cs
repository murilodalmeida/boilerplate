using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts;

internal sealed class GetPostWithCommentsEndpoint : IPostEndpoint
{
    record CommentResponse(string Id, string Content, string Author);
    record PostResponse(string Id, string Title, string Content, string Author, DateTime PublishedAt, List<CommentResponse> Comments);

    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet("{id}", HandleAsync)
        .MapToApiVersion(1)
        .Produces<PostResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound);

    private async Task<IResult> HandleAsync([FromRoute] string id)
    {
        await Task.Yield(); // Simulating async data fetching

        // Fake data (replace with database fetching logic)
        var post = new PostResponse(
            id,
            "Understanding Minimal APIs",
            "This post explains how to build lightweight APIs with ASP.NET.",
            "Alice Johnson",
            DateTime.UtcNow,
            new List<CommentResponse>
            {
                new("1", "Great article!", "Bob Smith"),
                new("2", "Very informative, thanks!", "Charlie Doe")
            }
        );

        return post is not null
            ? Results.Ok(post)
            : Results.Problem("Post not found", statusCode: StatusCodes.Status404NotFound);
    }
}
