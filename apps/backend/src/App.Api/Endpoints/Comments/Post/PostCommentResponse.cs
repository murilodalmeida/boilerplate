using System;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Post;

public sealed record PostCommentResponse(Guid Id)
{
    public static IResult From(CommentEntity comment) =>
        AppResponses.Created($"/comments/{comment.Id}", new PostCommentResponse(comment.Id));
}