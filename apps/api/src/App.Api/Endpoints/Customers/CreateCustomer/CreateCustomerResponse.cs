using FwksLabs.Boilerplate.Application.Features.Customers.CreateCustomer;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Boilerplate.App.Api.Endpoints.Customers.CreateCustomer;

public sealed record CreateCustomerResponse(string Id)
{
    public static CreateCustomerResponse From(CreateCustomerResult result) =>
        new(result.Id.Encode());
}