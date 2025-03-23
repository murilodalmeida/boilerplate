using FluentValidation;

namespace FwksLabs.Boilerplate.Core.Entities.Validators;

public sealed class ProductEntityValidator : AbstractValidator<ProductEntity>
{
    public ProductEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.Price).GreaterThan(0);
    }
}