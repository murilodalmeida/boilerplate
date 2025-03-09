using FluentValidation;
using FwksLabs.Boilerplate.Core.ValueObjects;

namespace FwksLabs.Boilerplate.Core.Entities.Validators;

public sealed class PostEntityValidator : AbstractValidator<PostEntity>
{
    public PostEntityValidator(
        IValidator<AuthorValueObject> authorValidator)
    {
        RuleFor(x => x.Author).SetValidator(authorValidator);

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();
    }
}