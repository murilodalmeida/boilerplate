using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using FwksLabs.Libs.Infra.OpenTelemetry.Exceptions;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Instrumentation.Http;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace FwksLabs.Libs.Infra.OpenTelemetry.Options;

public record TelemetryOptions
{
    public required string ServiceName { get; set; }
    public string? ServiceNamespace { get; set; }
    public string? ServiceVersion { get; set; }
    public string? ServiceInstanceId { get; set; }
    public Dictionary<string, object> Attributes { get; set; } = [];
    public string? CollectorEndpoint { get; set; }
    public OtlpExportProtocol CollectorEndpointProtocol { get; set; } = OtlpExportProtocol.Grpc;
    public MetricReaderTemporalityPreference TemporalityPreference { get; set; } = MetricReaderTemporalityPreference.Delta;
    public IEnumerable<ActivitySource> ActivitySources { get; set; } = [];
    public IEnumerable<string> IncomingRequestPathsFilter { get; set; } = [];
    public Func<HttpContext, bool>? IncomingRequestFilter { get; set; }
    public IEnumerable<string> OutgoingRequestPathsFilter { get; set; } = [];
    public Func<HttpRequestMessage, bool>? OutgoingRequestFilter { get; set; }
    public List<BaseProcessor<Activity>> TracingProcessors { get; set; } = [];


    internal void ConfigureResource(ResourceBuilder resource)
    {
        resource
            .AddService(ServiceName, ServiceNamespace, ServiceVersion, ServiceInstanceId is null, ServiceInstanceId)
            .AddAttributes(Attributes);
    }

    internal void ConfigureTracingExporter(OtlpExporterOptions options) =>
        ConfigureOtlpExporterOptions(options, $"{CollectorEndpoint}/v1/traces");

    internal void ConfigureMetricsExporter(OtlpExporterOptions otlpOptions, MetricReaderOptions metricsOptions)
    {
        ConfigureOtlpExporterOptions(otlpOptions, $"{CollectorEndpoint}/v1/metrics");

        metricsOptions.TemporalityPreference = TemporalityPreference;
    }

    internal void ConfigureHttpClientInstrumentation(HttpClientTraceInstrumentationOptions options)
    {
        options.FilterHttpRequestMessage = request =>
        {
            if (Activity.Current?.Parent is null || request.RequestUri is null)
                return false;

            var path = request.RequestUri.PathAndQuery.ToLowerInvariant();
            var excludedPaths = OutgoingRequestPathsFilter.Concat(["/health/"]);

            if (excludedPaths.Any(path.Contains) is false)
                return false;

            return OutgoingRequestFilter is null || OutgoingRequestFilter(request);
        };
    }

    internal void ConfigureAspNetCoreInstrumentation(AspNetCoreTraceInstrumentationOptions options)
    {
        options.RecordException = true;
        options.Filter = context =>
        {
            if (Activity.Current?.Parent is null || context.Request is null || context.Request.Path.Value is null)
                return false;

            var path = context.Request.Path.Value!.ToLowerInvariant();
            var excludedPaths = IncomingRequestPathsFilter.Concat(["/health/"]);

            if (excludedPaths.Any(path.Contains) is false)
                return false;

            return IncomingRequestFilter is null || IncomingRequestFilter(context);
        };
    }

    private void ConfigureOtlpExporterOptions(OtlpExporterOptions options, string url)
    {
        if (CollectorEndpoint is null || Uri.IsWellFormedUriString(CollectorEndpoint, UriKind.Absolute) is false)
            throw new TelemetryConfigurationException($"{CollectorEndpoint} is missing or misconfigured. Check the value and try again.");

        options.Endpoint = new Uri(url);
        options.Protocol = CollectorEndpointProtocol;
    }
}