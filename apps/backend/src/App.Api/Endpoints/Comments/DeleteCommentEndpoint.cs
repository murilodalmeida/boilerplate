using System.Threading.Tasks;
using FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments;

internal sealed class DeleteCommentEndpoint : ICommentEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapDelete("{id}", HandleAsync)
        .MapToApiVersion(1)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private async Task<IResult> HandleAsync([FromRoute] string id)
    {
        await Task.Yield();

        return Results.NoContent();
    }
}
