using System;
using FwksLabs.Boilerplate.Core.Entities;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Post;

public sealed record PostCommentRequest(
    Guid PostId, string Content, string AuthorName, string? AuthorEmail)
{
    public CommentEntity ToEntity() =>
        new()
        {
            PostId = PostId,
            Content = Content,
            Author = new(AuthorName, AuthorEmail)
        };
}