using System;

namespace FwksLabs.Libs.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumMetadataAttribute : Attribute
{
    public string? Symbol { get; set; }
    public string? Description { get; set; }
}