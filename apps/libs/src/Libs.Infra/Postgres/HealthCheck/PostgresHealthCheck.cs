using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace FwksLabs.Libs.Infra.Postgres.HealthCheck;

public sealed class PostgresHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var dataSource = serviceProvider
                .GetRequiredKeyedService<NpgsqlDataSource>(nameof(PostgresHealthCheck));

            var connection = dataSource.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1;";
            command.CommandTimeout = 60;

            var result = await command.ExecuteScalarAsync(cancellationToken);

            return result is 1
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
        }
    }
}