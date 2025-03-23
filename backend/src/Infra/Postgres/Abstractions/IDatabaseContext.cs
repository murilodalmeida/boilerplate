using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Boilerplate.Infra.Postgres.Abstractions;

public interface IDatabaseContext
{
    DbSet<CustomerEntity> Customers { get; set; }
    DbSet<OrderEntity> Orders { get; set; }
    DbSet<ProductEntity> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}