using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Core.ValueObjects;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.Create;

public sealed record CreatePostRequest(string Title, string Content, AuthorValueObject Author)
{
    internal PostEntity ToEntity()
    {
        return new PostEntity
        {
            Author = Author,
            Title = Title,
            Content = Content
        };
    }
}