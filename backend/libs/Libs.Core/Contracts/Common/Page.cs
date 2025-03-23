using System;
using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Contracts.Common;

public sealed record Page<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / (PageSize < 1 ? TotalItems : PageSize));
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
    public IReadOnlyCollection<T> Items { get; set; } = [];

    public static Page<T> From(int pageNumber, int pageSize, int totalItems, IReadOnlyCollection<T> items) =>
        new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize < 1 ? -1 : pageSize,
            TotalItems = totalItems,
            Items = items
        };
}