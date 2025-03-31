using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Boilerplate.Core.ValueObject;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;

namespace FwksLabs.Boilerplate.Core.Entities;

public sealed class OrderEntity : IEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required Guid CustomerId { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? PaymentDate { get; set; }
    public ICollection<OrderProductValueObject> Products { get; set; } = [];
    public decimal Total => Products.Sum(p => p.Total);
}