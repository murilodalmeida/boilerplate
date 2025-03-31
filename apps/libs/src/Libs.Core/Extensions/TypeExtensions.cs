using System;
using System.Collections.Generic;
using System.Linq;

namespace FwksLabs.Libs.Core.Extensions;

public static class TypeExtensions
{
    public static List<TCast> CreateInstancesOf<TBase, TCast>(this Type typeTarget) =>
        [.. typeTarget.Assembly
            .GetTypes()
            .Where(t => typeof(TBase).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<TCast>()];

    public static void ConfigureFromType<T>(this Type type)
    {
        _ = type.Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(T)))
            .Select(Activator.CreateInstance);
    }
}