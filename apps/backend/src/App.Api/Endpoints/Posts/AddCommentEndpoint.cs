using System;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Core.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts;

internal sealed class AddCommentEndpoint : IPostEndpoint
{
    record AddCommentRequest(string Content, AuthorValueObject Author);
    record CommentResponse(string Id);

    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost("{id}/comment", HandleAsync)
        .MapToApiVersion(1)
        .Produces<CommentResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private async Task<IResult> HandleAsync([FromRoute] string id, AddCommentRequest request)
    {
        await Task.Yield();

        var commentId = Guid.NewGuid().ToString();
        return Results.Created($"/comments/{commentId}", new CommentResponse(commentId));
    }
}
