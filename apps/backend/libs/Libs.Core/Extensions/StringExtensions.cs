using System;

namespace FwksLabs.Libs.Core.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string? value) =>
        !string.IsNullOrWhiteSpace(value);

    public static string IfEmpty(this string? value, string fallback) =>
        string.IsNullOrWhiteSpace(value) ? fallback : value;

    public static bool EqualsTo(this string? value, string target) =>
        string.Equals(value, target, StringComparison.InvariantCultureIgnoreCase);
}