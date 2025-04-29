using System;
using FwksLabs.Boilerplate.Core.Entities;

namespace FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;

public sealed record GetCustomersResult(Guid Id, string Name, string? PhoneNumber, string? Email)
{
    public static GetCustomersResult From(CustomerEntity customer) =>
        new(customer.Id, customer.Name, customer.PhoneNumber, customer.Email);
}