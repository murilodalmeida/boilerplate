using System;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;

namespace FwksLabs.Boilerplate.Core.Entities;

public sealed class CustomerEntity : IEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}