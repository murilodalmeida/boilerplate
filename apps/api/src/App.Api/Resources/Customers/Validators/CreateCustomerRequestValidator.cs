using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Resources.Customers.Requests;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Boilerplate.App.Api.Resources.Customers.Validators;

public sealed class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.PhoneNumber).PhoneNumber().When(x => x.PhoneNumber is not null);

        RuleFor(x => x.Email).EmailAddress().When(x => x.Email is not null);
    }
}