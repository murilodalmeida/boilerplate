using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints<TEndpointGroup>(
        this IEndpointRouteBuilder builder, string prefix, string[]? tags = null, int[]? problems = null, double[]? versions = null)
        where TEndpointGroup : IEndpoint
    {
        prefix = prefix.Kebaberize();
        tags ??= [prefix.Dehumanize()];

        builder
            .MapGroup($"v{{version:apiVersion}}/{prefix}")
            .WithTags(tags)
            .WithVersionSet(versions)
            .WithProblems(problems)
            .WithEndpoints<TEndpointGroup>();
    }

    private static RouteGroupBuilder WithVersionSet(this RouteGroupBuilder builder, double[]? versions)
    {
        versions ??= [1.0];

        var set = builder.NewApiVersionSet().ReportApiVersions();

        foreach (var ver in versions)
            set.HasApiVersion(new(ver));

        builder.WithApiVersionSet(set.Build());

        return builder;
    }

    private static RouteGroupBuilder WithProblems(this RouteGroupBuilder builder, int[]? problems)
    {
        var uniqueProblems = new List<int>(problems ?? [])
            .Concat([
                StatusCodes.Status422UnprocessableEntity,
                StatusCodes.Status500InternalServerError
            ]).Distinct().ToList();

        foreach (var problem in uniqueProblems)
            builder.ProducesProblem(problem);

        return builder;
    }

    private static void WithEndpoints<TEndpointGroup>(this IEndpointRouteBuilder builder)
    {
        var endpoints = typeof(TEndpointGroup).CreateInstancesOf<TEndpointGroup, IEndpoint>();

        foreach (var endpoint in endpoints)
            endpoint.Map(builder);
    }
}