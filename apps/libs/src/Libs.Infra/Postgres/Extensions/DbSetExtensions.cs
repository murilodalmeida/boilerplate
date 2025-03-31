using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Contracts.Requests;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class DbSetExtensions
{
    public static Task<PagedResult<TEntity>> GetPageAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        PageRequest request,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return queryable.GetPageAsync(request, _ => true, cancellationToken);
    }

    public static async Task<PagedResult<TEntity>> GetPageAsync<TEntity>(
        this IQueryable<TEntity> dbSet,
        PageRequest request,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        var query = request.PageSize < 1
            ? dbSet.AsNoTracking().Where(predicate)
            : dbSet.AsNoTracking().Where(predicate).Skip(request.GetSkip()).Take(request.PageSize);

        return new PagedResult<TEntity>(
            request.PageNumber,
            request.PageSize,
            await query.ToListAsync(cancellationToken),
            await dbSet.CountAsync(predicate, cancellationToken));
    }
}