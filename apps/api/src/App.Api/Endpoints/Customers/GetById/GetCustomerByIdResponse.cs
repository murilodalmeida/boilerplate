using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.GetById;

public sealed record GetCustomerByIdResponse(string Id, string Name, string? PhoneNumber, string? Email)
{
    public static IResult ToResponse(CustomerEntity customer) =>
        AppResponses.Ok(
            new GetCustomerByIdResponse(
                customer.Id.Encode(), customer.Name, customer.PhoneNumber, customer.Email));
}