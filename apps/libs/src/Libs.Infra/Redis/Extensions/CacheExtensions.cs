using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Contracts.Common;
using Microsoft.Extensions.Caching.Hybrid;

namespace FwksLabs.Libs.Infra.Redis.Extensions;

public static class CacheExtensions
{
    public static ValueTask<TValue> GetOrSetAsync2<TValue>(
        this HybridCache cache, CacheKey key, Func<CancellationToken, ValueTask<TValue>> factory, CancellationToken cancellationToken = default) =>
            cache.GetOrCreateAsync(key.Name, factory, null, key.Tags, cancellationToken);
}