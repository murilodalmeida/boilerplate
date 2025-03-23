using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Infra.MongoDb.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.MongoDb.HealthCheck;

public sealed class MongoDbHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var mongoContext = serviceProvider
                .GetRequiredKeyedService<MongoDbHealthCheckContext>(nameof(MongoDbHealthCheck));

            if (mongoContext.IsAdmin)
            {
                using var cursor = await mongoContext.Client.ListDatabaseNamesAsync(cancellationToken);

                _ = await cursor.FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                var command = new BsonDocumentCommand<BsonDocument>([.. BsonDocument.Parse("{ping:1}")]);

                for (int current = 1; current <= mongoContext.MaxRetries; current++)
                {
                    try
                    {
                        await mongoContext.Client
                            .GetDatabase(mongoContext.DatabaseName)
                            .RunCommandAsync(command, cancellationToken: cancellationToken);

                        break;
                    }
                    catch
                    {
                        if (current == mongoContext.MaxRetries)
                            throw;

                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
        }
    }
}