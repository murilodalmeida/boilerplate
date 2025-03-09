using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Boilerplate.Infra.Postgres.Abstractions;

public interface IDatabaseContext
{
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}