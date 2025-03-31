using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Infra.LiteDb.Extensions;
using LiteDB;

namespace FwksLabs.Boilerplate.Infra.LiteDb;

public sealed class DatabaseContext(ILiteDatabase database) : IDatabaseContext
{
    private readonly ILiteDatabase database = database;

    private ILiteCollection<CustomerEntity>? customers;
    private ILiteCollection<OrderEntity>? orders;
    private ILiteCollection<ProductEntity>? products;

    public ILiteCollection<CustomerEntity> Customers => customers ??= database.GetNamedCollection<CustomerEntity>();
    public ILiteCollection<OrderEntity> Orders => orders ??= database.GetNamedCollection<OrderEntity>();
    public ILiteCollection<ProductEntity> Products => products ??= database.GetNamedCollection<ProductEntity>();
}