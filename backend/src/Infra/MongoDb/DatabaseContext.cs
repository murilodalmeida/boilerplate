using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.Infra.MongoDb.Extensions;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb;

public sealed class DatabaseContext(IMongoDatabase database) : IDatabaseContext
{
    private readonly IMongoDatabase database = database;

    private IMongoCollection<CustomerEntity>? customers;
    private IMongoCollection<OrderEntity>? orders;
    private IMongoCollection<ProductEntity>? products;

    public IMongoCollection<CustomerEntity> Customers => customers ??= database.GetNamedCollection<CustomerEntity>();
    public IMongoCollection<OrderEntity> Orders => orders ??= database.GetNamedCollection<OrderEntity>();
    public IMongoCollection<ProductEntity> Products => products ??= database.GetNamedCollection<ProductEntity>();
}