using FwksLabs.Boilerplate.Core.Entities;

namespace FwksLabs.Boilerplate.App.Api.Resources.Customers.Requests;

public sealed record CreateCustomerRequest(string Name, string? PhoneNumber, string? Email)
{
    public CustomerEntity ToCustomer() =>
        new()
        {
            Name = Name,
            PhoneNumber = PhoneNumber,
            Email = Email
        };
}