using System;

namespace FwksLabs.Libs.Core.Extensions;

public static class EnumExtensions
{
    public static T AsEnum<T>(this string? value, T? fallbackValue = null) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, true, out var result))
            return result;

        if (fallbackValue is not null)
            return fallbackValue.Value;

        throw new InvalidCastException($"'{value}' is not a valid '{typeof(T).Name}' enum value.");
    }

    public static int GetId(this Enum value) =>
        Convert.ToInt32(value);
}