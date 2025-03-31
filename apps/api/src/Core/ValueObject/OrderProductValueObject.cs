using System;

namespace FwksLabs.Boilerplate.Core.ValueObject;

public record OrderProductValueObject(Guid ProductId, int Quantity, decimal Total);