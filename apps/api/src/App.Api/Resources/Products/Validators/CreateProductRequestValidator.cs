using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Resources.Products.Requests;

namespace FwksLabs.Boilerplate.App.Api.Resources.Products.Validators;

public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Price).GreaterThan(0);
    }
}