using FluentValidation;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Comments.Post;

public sealed class PostCommentRequestValidator : AbstractValidator<PostCommentRequest>
{
    public PostCommentRequestValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();

        RuleFor(x => x.AuthorName).NotEmpty();

        RuleFor(x => x.AuthorEmail).EmailAddress().When(x => x.AuthorEmail is not null);
    }
}