using System;

namespace FwksLabs.Libs.AspNetCore.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class EndpointResourceAttribute(string route) : Attribute
{
    public string Route { get; init; } = route;
    public string[]? Tags { get; init; }
    public int[]? Problems { get; init; }
    public double[]? Versions { get; init; }
}