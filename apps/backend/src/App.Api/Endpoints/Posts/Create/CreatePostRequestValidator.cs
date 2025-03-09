using FluentValidation;
using FwksLabs.Boilerplate.Core.ValueObjects;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Posts.Create;

public sealed class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator(
        IValidator<AuthorValueObject> authorValidator)
    {
        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();

        RuleFor(x => x.Author).SetValidator(authorValidator);
    }
}