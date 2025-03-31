using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Encoders;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.Create;

public sealed record CreateCustomerResponse(string Id)
{
    public static IResult ToResponse(CustomerEntity customer) =>
        AppResponses.Created(new CreateCustomerResponse(customer.Id.Encode()));
}