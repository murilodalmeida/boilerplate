using System;
using System.Reflection;
using FwksLabs.Libs.Core.Attributes;

namespace FwksLabs.Libs.Core.Extensions;

public static class EnumMetadataAttributeExtensions
{
    public static EnumMetadataAttribute? GetMetadata(this Enum value) =>
        value.GetType().GetField(value.ToString())?.GetCustomAttribute<EnumMetadataAttribute>();

    public static string GetSymbol(this Enum value) =>
        value.GetMetadata()?.Symbol ?? value.ToString();

    public static string GetDescription(this Enum value) =>
        value.GetMetadata()?.Description ?? value.ToString();
}