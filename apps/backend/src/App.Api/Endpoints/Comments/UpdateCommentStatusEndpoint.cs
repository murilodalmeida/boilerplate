using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using FwksLabs.Boilerplate.Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments;

internal sealed class UpdateCommentStatusEndpoint : ICommentEndpoint
{
    record UpdateCommentStatusRequest(PostModerationStatus Status);

    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPatch("{id}/status", HandleAsync)
        .MapToApiVersion(1)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private async Task<IResult> HandleAsync([FromRoute] string id, UpdateCommentStatusRequest request)
    {
        await Task.Yield();

        return Results.NoContent();
    }
}
