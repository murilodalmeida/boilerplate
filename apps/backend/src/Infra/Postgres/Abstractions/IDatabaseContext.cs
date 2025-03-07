using System.Threading;
using System.Threading.Tasks;

namespace FwksLabs.Boilerplate.Infra.Postgres.Abstractions;

public interface IDatabaseContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}