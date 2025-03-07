using System.Linq;
using FwksLabs.Libs.Infra.OpenTelemetry.Options;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace FwksLabs.Libs.Infra.OpenTelemetry.Configuration;

public static class OpenTelemetryConfiguration
{
    public static OpenTelemetryBuilder ConfigureTracing(this OpenTelemetryBuilder builder, TelemetryOptions options) =>
        builder
            .WithTracing(builder =>
            {
                builder
                    .ConfigureResource(options.ConfigureResource)
                    .AddOtlpExporter(options.ConfigureTracingExporter)
                    .AddSource([.. options.ActivitySources.Select(x => x.Name)])
                    .AddHttpClientInstrumentation(options.ConfigureHttpClientInstrumentation)
                    .AddAspNetCoreInstrumentation(options.ConfigureAspNetCoreInstrumentation)
                    .SetErrorStatusOnException()
                    .SetSampler<AlwaysOnSampler>();

                foreach (var processor in options.TracingProcessors)
                    builder.AddProcessor(processor);
            });

    public static OpenTelemetryBuilder ConfigureMetrics(this OpenTelemetryBuilder builder, TelemetryOptions options) =>
        builder
            .WithMetrics(builder => builder
                .ConfigureResource(options.ConfigureResource)
                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter(options.ConfigureMetricsExporter));
}