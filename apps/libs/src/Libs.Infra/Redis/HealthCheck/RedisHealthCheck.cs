using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace FwksLabs.Libs.Infra.Redis.HealthCheck;

public sealed class RedisHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var connection = serviceProvider
                .GetRequiredKeyedService<IConnectionMultiplexer>(nameof(RedisHealthCheck));

            var endpoints = connection.GetEndPoints(true);

            var failedEndpoints = 0;
            var errors = new Dictionary<string, object>();

            try
            {
                foreach (var endpoint in endpoints)
                {
                    var server = connection.GetServer(endpoint);

                    _ = await server.PingAsync();
                }
            }
            catch (RedisConnectionException redisException)
            {
                failedEndpoints++;
                errors.Add("Redis Error", redisException.FailureType.Humanize());
            }
            catch (Exception)
            {
                failedEndpoints++;
                errors.Add("Connection Failed", "Failed to send ping request");
            }

            if (errors.Count > 0)
            {
                var status = failedEndpoints < endpoints.Length ? HealthStatus.Degraded : HealthStatus.Unhealthy;

                return new HealthCheckResult(status, AppProperties.Errors.UnreachableResource, null, errors);
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
        }
    }
}