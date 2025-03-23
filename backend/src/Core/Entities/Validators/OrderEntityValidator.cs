using FluentValidation;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Boilerplate.Core.Entities.Validators;

public sealed class OrderEntityValidator : AbstractValidator<OrderEntity>
{
    public OrderEntityValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();

        RuleFor(x => x.CreationDate).NotInThePast();

        RuleFor(x => x.PaymentDate)
            .Must((order, paymentDate) => paymentDate < order.CreationDate)
            .WithMessage("Payment Date can't be lower than the Order Creation Date.")
            .When(x => x.PaymentDate is not null);

        RuleFor(x => x.Products).NotEmpty();
    }
}