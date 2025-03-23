namespace FwksLabs.Boilerplate.App.Api.Endpoints.Orders.Create;

public sealed record OrderProductRequest(string ProductId, int Quantity, decimal Total);