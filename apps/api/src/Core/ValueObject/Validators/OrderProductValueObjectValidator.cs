using FluentValidation;

namespace FwksLabs.Boilerplate.Core.ValueObject.Validators;

public sealed class OrderProductValueObjectValidator : AbstractValidator<OrderProductValueObject>
{
    public OrderProductValueObjectValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Quantity).GreaterThan(0);

        RuleFor(x => x.Total).GreaterThan(0);
    }
}