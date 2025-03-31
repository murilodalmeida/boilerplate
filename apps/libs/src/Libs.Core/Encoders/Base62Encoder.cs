using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FwksLabs.Libs.Core.Encoders;

public static class Base62Encoder
{
    private static string DefaultCharacterSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static void OverwriteCharacterSet(string characterSet) =>
        DefaultCharacterSet = characterSet;

    public static string Encode(this Guid original) =>
        ToBase62(original);

    public static string ToBase62(this Guid original) =>
        original.ToByteArray().ToBase62();

    public static string ToBase62(this short original) =>
        BitConverter.GetBytes(original).ToBase62();

    public static string ToBase62(this int original) =>
        BitConverter.GetBytes(original).ToBase62();

    public static string ToBase62(this long original) =>
        BitConverter.GetBytes(original).ToBase62();

    public static string ToBase62(this string original) =>
        Encoding.UTF8.GetBytes(original).ToBase62();

    public static string ToBase62(this byte[] original)
    {
        var characterSet = DefaultCharacterSet;
        var arr = Array.ConvertAll(original, t => (int)t);

        var converted = BaseConvert(arr, 256, 62);
        var builder = new StringBuilder();

        foreach (var t in converted)
            builder.Append(characterSet[t]);

        return builder.ToString();
    }

    public static T FromBase62<T>(this string base62)
    {
        var array = base62.FromBase62();

        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.String => ConvertValue(Encoding.UTF8.GetString(array)),

            TypeCode.Int16 => ConvertValue(BitConverter.ToInt16(array, 0)),

            TypeCode.Int32 => ConvertValue(BitConverter.ToInt32(array, 0)),

            TypeCode.Int64 => ConvertValue(BitConverter.ToInt64(array, 0)),

            TypeCode.Object when typeof(T) == typeof(Guid) => ConvertValue(new Guid(array)),

            _ => throw new Exception($"Type of {typeof(T)} does not support."),
        };

        static T ConvertValue(object value) =>
            (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
    }

    public static byte[] FromBase62(this string base62)
    {
        if (string.IsNullOrWhiteSpace(base62))
            throw new ArgumentNullException(nameof(base62));

        var characterSet = DefaultCharacterSet;

        var arr = Array.ConvertAll(base62.ToCharArray(), characterSet.IndexOf);

        var converted = BaseConvert(arr, 62, 256);

        return Array.ConvertAll(converted, Convert.ToByte);
    }

    public static Guid Decode(this string base62) =>
        base62.FromBase62<Guid>();

    private static int[] BaseConvert(int[] source, int sourceBase, int targetBase)
    {
        var result = new List<int>();
        var leadingZeroCount = Math.Min(source.TakeWhile(x => x == 0).Count(), source.Length - 1);
        int count;

        while ((count = source.Length) > 0)
        {
            var quotient = new List<int>();
            var remainder = 0;

            for (var i = 0; i != count; i++)
            {
                var accumulator = source[i] + remainder * sourceBase;
                var digit = accumulator / targetBase;
                remainder = accumulator % targetBase;
                if (quotient.Count > 0 || digit > 0)
                    quotient.Add(digit);
            }

            result.Insert(0, remainder);

            source = [.. quotient];
        }

        result.InsertRange(0, Enumerable.Repeat(0, leadingZeroCount));

        return [.. result];
    }
}