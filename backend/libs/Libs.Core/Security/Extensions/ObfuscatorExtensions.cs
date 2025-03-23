namespace FwksLabs.Libs.Core.Security.Extensions;

public static class ObfuscatorExtensions
{
    public static string Encode(this int value) => Obfuscator.Encode(value);
    public static string Encode<T>(this int value) => Obfuscator.Encode<T>(value);
    public static int Decode(this string value) => Obfuscator.Decode(value);
    public static int Decode<T>(this string value) => Obfuscator.Decode<T>(value);
}