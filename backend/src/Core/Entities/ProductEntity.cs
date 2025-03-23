using System;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;

namespace FwksLabs.Boilerplate.Core.Entities;

public sealed class ProductEntity : IEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}