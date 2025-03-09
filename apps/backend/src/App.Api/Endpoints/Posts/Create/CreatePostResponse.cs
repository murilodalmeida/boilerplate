using System;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.Create;

public sealed record CreatePostResponse(Guid Id)
{
    public static IResult From(PostEntity post) =>
        AppResponses.Ok(new CreatePostResponse(post.Id));
}