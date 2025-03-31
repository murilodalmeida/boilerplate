using System;
using System.Collections.Concurrent;
using System.Linq;
using FwksLabs.Libs.Core.Security.Abstractions;
using Sqids;

namespace FwksLabs.Libs.Core.Security;

public class Obfuscator
{
    private static readonly int _seed = 1234567890;
    private static readonly string _alphabet = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static readonly int _minLength = 5;

    private static readonly ConcurrentDictionary<Type, SqidsEncoder<int>> _encoders = [];

    public static Obfuscator ConfigureForAssembly<TAssembly>(int? seed, string? alphabet, int? minLength)
    {
        seed ??= _seed;
        alphabet ??= _alphabet;
        minLength ??= _minLength;

        var types = typeof(TAssembly).Assembly
            .GetTypes()
            .Where(x => x.IsInterface is false && typeof(IObfuscable).IsAssignableFrom(x))
            .ToList();

        _ = _encoders.TryAdd(typeof(Obfuscator), CreateEncoder(seed.Value, alphabet, minLength.Value));

        foreach (var type in types)
        {
            if (_encoders.ContainsKey(type))
                continue;

            _ = _encoders.TryAdd(type, CreateEncoder(CalcSeed(), alphabet, minLength.Value));

            int CalcSeed() => Math.Abs(seed.Value * (_encoders.Count + 1));
        }

        return new();
    }

    public static string Encode<T>(int value) =>
        _encoders.TryGetValue(typeof(T), out var encoder)
            ? encoder.Encode(value)
            : throw new InvalidOperationException($"No encoder found for type {typeof(T).Name}");

    public static string Encode(int value) =>
        Encode<Obfuscator>(value);

    public static int Decode<T>(string value) =>
        _encoders.TryGetValue(typeof(T), out var encoder)
            ? encoder.Decode(value)[0]
            : throw new InvalidOperationException($"No decoder found for type {typeof(T).Name}");

    public static int Decode(string value) =>
        Decode<Obfuscator>(value);

    private static SqidsEncoder<int> CreateEncoder(int seed, string alphabet, int minLength)
    {
        var random = new Random(seed);
        var shuffledAlphabet = new string([.. alphabet.OrderBy(_ => random.Next())]);

        return new(new SqidsOptions { Alphabet = shuffledAlphabet, MinLength = minLength });
    }
}