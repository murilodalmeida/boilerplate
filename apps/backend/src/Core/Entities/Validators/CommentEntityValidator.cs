using FluentValidation;
using FwksLabs.Boilerplate.Core.ValueObjects;

namespace FwksLabs.Boilerplate.Core.Entities.Validators;

public sealed class CommentEntityValidator : AbstractValidator<CommentEntity>
{
    public CommentEntityValidator(
        IValidator<AuthorValueObject> authorValidator)
    {
        RuleFor(x => x.Author).SetValidator(authorValidator);

        RuleFor(x => x.Content).NotEmpty();
    }
}