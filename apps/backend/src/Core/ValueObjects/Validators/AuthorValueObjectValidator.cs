using FluentValidation;

namespace FwksLabs.Boilerplate.Core.ValueObjects.Validators;

public sealed class AuthorValueObjectValidator : AbstractValidator<AuthorValueObject>
{
    public AuthorValueObjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Email).EmailAddress().When(x => x is not null);
    }
}