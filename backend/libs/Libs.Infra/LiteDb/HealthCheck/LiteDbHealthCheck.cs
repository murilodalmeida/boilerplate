using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.LiteDb.HealthCheck;

public sealed class LiteDbHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var litedatabase = serviceProvider
                .GetRequiredKeyedService<ILiteDatabase>(nameof(LiteDbHealthCheck));

            _ = litedatabase.GetCollectionNames();

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return Task.FromResult(
                new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception));
        }
    }
}