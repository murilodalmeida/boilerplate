using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Core.Abstractions.Common;
using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.Application.Features.Customers.GetCustomers;

public sealed class GetCustomersQueryHandler(
    IDatabaseContext dbContext) : IQueryHandler<GetCustomersQuery, GetCustomersResult>
{
    public async Task<PagedResult<GetCustomersResult>> HandleAsync(GetCustomersQuery query, CancellationToken cancellation)
    {
        await Task.Yield();

        var customers = dbContext.Customers.FindAll().ToList();

        return new(query.PageNumber, query.PageSize, customers.Count, [.. customers.Select(GetCustomersResult.From)]);
    }
}