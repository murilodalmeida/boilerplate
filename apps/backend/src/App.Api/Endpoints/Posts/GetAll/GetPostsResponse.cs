using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.GetAll;

public sealed record PostCommentResponse(string Content, string AuthorName)
{
    public static IReadOnlyCollection<PostCommentResponse> From(ICollection<CommentEntity> comments) =>
        [.. comments.Select(x => new PostCommentResponse(x.Content, x.Author.Name))];
}

public sealed record PostResponse(Guid Id, string Title, string Content, string PublishedAt, string AuthorName, IReadOnlyCollection<PostCommentResponse> Comments)
{
    public static PostResponse From(PostEntity post) => new(post.Id, post.Title, post.Content, post.PublishedAt.Humanize(true), post.Author.Name, PostCommentResponse.From(post.Comments));
}

public sealed record GetPostsResponse(Dictionary<string, IReadOnlyCollection<PostResponse>> Data)
{
    public static IResult From(Dictionary<string, IReadOnlyCollection<PostResponse>> data) =>
        AppResponses.Ok(new GetPostsResponse(data));
}