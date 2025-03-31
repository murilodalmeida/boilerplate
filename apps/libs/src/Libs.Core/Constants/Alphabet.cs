using System;
using System.Linq;

namespace FwksLabs.Libs.Core.Constants;

public static class Alphabet
{
    public const string Default = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYWZabcdefghijklmnopqrstuvxywz";

    public static string Randomize()
    {
        return new string([.. Default.OrderBy(x => Random.Shared.Next())]);
    }
}
