using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.HealthCheck;

public sealed class HttpServiceHealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient(context.Registration.Name);

            var response = await httpClient.GetAsync(string.Empty, cancellationToken);

            return response.IsSuccessStatusCode ? HealthCheckResult.Healthy() : Failure();
        }
        catch (Exception ex)
        {
            return Failure(ex);
        }

        HealthCheckResult Failure(Exception? exception = default) =>
            new(context.Registration.FailureStatus, AppProperties.Errors.UnreachableResource, exception);
    }
}