using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Contracts.Requests;
using FwksLabs.Libs.Core.Extensions;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.MongoDb.Extensions;

public static class MongoDatabaseExtension
{
    public static IMongoCollection<T> GetNamedCollection<T>(this IMongoDatabase database) =>
        database.GetCollection<T>(typeof(T).Name.PluralizeEntity());

    public static Task<PagedResult<TEntity>> GetPageAsync<TEntity>(
        this IMongoCollection<TEntity> collection,
        PageRequest request,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return collection.GetPageAsync(request, _ => true, cancellationToken);
    }

    public static async Task<PagedResult<TEntity>> GetPageAsync<TEntity>(
        this IMongoCollection<TEntity> collection,
        PageRequest request,
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        var query = request.PageSize < 1
            ? collection.Find(_ => true, null)
            : collection.Find(predicate, null).Skip(request.GetSkip()).Limit(request.PageSize);

        var count = await collection.CountDocumentsAsync(Builders<TEntity>.Filter.Empty, null, cancellationToken);

        return new PagedResult<TEntity>(
            request.PageNumber,
            request.PageSize,
            await query.ToListAsync(cancellationToken),
            (int)count);
    }
}