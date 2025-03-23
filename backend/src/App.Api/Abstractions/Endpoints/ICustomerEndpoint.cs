using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Attributes;

namespace FwksLabs.Boilerplate.App.Api.Abstractions.Endpoints;

[EndpointResource("customers")]
public interface ICustomerEndpoint : IEndpoint;