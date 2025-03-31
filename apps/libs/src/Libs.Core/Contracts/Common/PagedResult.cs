using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Contracts.Common;

public sealed record PagedResult<TEntity>(int PageNumber, int PageSize, IReadOnlyCollection<TEntity> Items, int TotalItems);