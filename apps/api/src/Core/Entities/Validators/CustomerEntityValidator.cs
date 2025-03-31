using FluentValidation;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Boilerplate.Core.Entities.Validators;

public sealed class CustomerEntityValidator : AbstractValidator<CustomerEntity>
{
    public CustomerEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.PhoneNumber).PhoneNumber().When(x => x.PhoneNumber.IsNotEmpty());

        RuleFor(x => x.Email).EmailAddress().When(x => x.Email.IsNotEmpty());
    }
}