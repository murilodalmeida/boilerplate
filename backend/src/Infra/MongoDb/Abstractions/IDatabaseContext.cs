using FwksLabs.Boilerplate.Core.Entities;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;

public interface IDatabaseContext
{
    IMongoCollection<CustomerEntity> Customers { get; }
    IMongoCollection<OrderEntity> Orders { get; }
    IMongoCollection<ProductEntity> Products { get; }
}