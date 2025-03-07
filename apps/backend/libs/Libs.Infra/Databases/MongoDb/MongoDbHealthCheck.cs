using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.Databases.MongoDb;

public sealed class MongoDbHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var cursor = await serviceProvider
                .GetRequiredKeyedService<IMongoClient>(nameof(MongoDbHealthCheck))
                .ListDatabaseNamesAsync(cancellationToken);

            _ = await cursor.FirstOrDefaultAsync(cancellationToken);

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
        }
    }
}