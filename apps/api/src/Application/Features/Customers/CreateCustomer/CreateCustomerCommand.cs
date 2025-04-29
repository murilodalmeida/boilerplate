using FwksLabs.Boilerplate.Core.Entities;

namespace FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(string Name, string? PhoneNumber, string? Email)
{
    public CustomerEntity ToEntity() =>
        new() { Name = Name, PhoneNumber = PhoneNumber, Email = Email };
}