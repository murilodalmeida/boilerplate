namespace FwksLabs.Boilerplate.App.Api.Resources.Orders.Requests;

public sealed record OrderProductRequest(string ProductId, int Quantity, decimal Total);