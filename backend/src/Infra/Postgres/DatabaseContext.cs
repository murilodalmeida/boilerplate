using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Infra.Abstractions;
using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Boilerplate.Infra.Postgres;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDatabaseContext
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfra).Assembly);
    }
}