using FluentValidation;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Products.Create;

public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Price).GreaterThan(0);
    }
}