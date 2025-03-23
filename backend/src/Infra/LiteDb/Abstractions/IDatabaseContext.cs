using FwksLabs.Boilerplate.Core.Entities;
using LiteDB;

namespace FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;

public interface IDatabaseContext
{
    ILiteCollection<CustomerEntity> Customers { get; }
    ILiteCollection<OrderEntity> Orders { get; }
    ILiteCollection<ProductEntity> Products { get; }
}