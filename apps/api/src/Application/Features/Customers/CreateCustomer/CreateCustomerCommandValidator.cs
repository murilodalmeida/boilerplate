using FluentValidation;
    
namespace FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {

    }
}