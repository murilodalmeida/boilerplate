using System;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Core.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts;

internal sealed class CreatePostEndpoint : IPostEndpoint
{
    record CreatePostRequest(string Title, string Content, AuthorValueObject Author);
    record PostResponse(string Id);

    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost("", HandleAsync)
        .MapToApiVersion(1)
        .Produces<PostResponse>(StatusCodes.Status201Created);

    private async Task<IResult> HandleAsync(CreatePostRequest request)
    {
        await Task.Yield(); // Simulate async work

        var newPostId = Guid.NewGuid().ToString();
        return Results.Created($"/posts/{newPostId}", new PostResponse(newPostId));
    }
}