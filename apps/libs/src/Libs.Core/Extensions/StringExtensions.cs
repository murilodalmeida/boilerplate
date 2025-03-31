using System;
using System.Globalization;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string? input) =>
        string.IsNullOrWhiteSpace(input);

    public static bool IsNotEmpty(this string? input) =>
        !string.IsNullOrWhiteSpace(input);

    public static string WhenEmpty(this string? input, string fallback) =>
        string.IsNullOrWhiteSpace(input) ? fallback : input;

    public static bool EqualsTo(this string? input, string target) =>
        string.Equals(input, target, StringComparison.InvariantCultureIgnoreCase);

    public static string PluralizeEntity(this string name) =>
        name[..^"Entity".Length].Pluralize();

    public static string NormalizeString(this string? input)
    {
        if (input is null || input.IsEmpty())
            return string.Empty;

        var normalized = new string([.. input
            .Normalize(System.Text.NormalizationForm.FormD)
            .Where(x => CharUnicodeInfo.GetUnicodeCategory(x) != UnicodeCategory.NonSpacingMark)]);

        return AppRegex.Alphanumeric().Replace(normalized, string.Empty);
    }

    public static string Codify(this string? input) =>
        input.NormalizeString().Underscore().ToUpperInvariant();
}