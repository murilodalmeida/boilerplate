using FluentValidation;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.Create;

public sealed class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.PhoneNumber).PhoneNumber().When(x => x.PhoneNumber is not null);

        RuleFor(x => x.Email).EmailAddress().When(x => x.Email is not null);
    }
}