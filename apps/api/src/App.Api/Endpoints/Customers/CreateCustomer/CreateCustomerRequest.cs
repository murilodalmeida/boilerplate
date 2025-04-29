using FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.CreateCustomer;

public sealed record CreateCustomerRequest(string Name, string? PhoneNumber, string? Email)
{
    public CreateCustomerCommand ToCommand() => new(Name, PhoneNumber, Email);
}