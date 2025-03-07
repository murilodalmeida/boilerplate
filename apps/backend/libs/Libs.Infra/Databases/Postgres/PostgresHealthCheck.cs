using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace FwksLabs.Libs.Infra.Databases.Postgres;

public sealed class PostgresHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var command = serviceProvider
                .GetRequiredKeyedService<NpgsqlDataSource>(nameof(PostgresHealthCheck));
                
            command.CreateCommand("SELECT 1;");

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
        }
    }
}