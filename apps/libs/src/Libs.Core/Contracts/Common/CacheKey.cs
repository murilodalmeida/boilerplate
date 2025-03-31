using System.Linq;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;

namespace FwksLabs.Libs.Core.Contracts.Common;

public record CacheKey
{
    private CacheKey() { }

    public required string Name { get; init; }
    public string[] Tags { get; private set; } = [];

    public static CacheKey For<T>(string key, string? value = null, params string[] tags)
    {
        var identifier = typeof(T).Name.ToLower();

        key = $"{identifier}:{key}";

        return new()
        {
            Name = (value.IsEmpty() ? key : $"{key}:{value}").Kebaberize(),
            Tags = [.. tags.Select(tag => tag.Kebaberize())]
        };
    }

    public override string ToString() => Name;
}