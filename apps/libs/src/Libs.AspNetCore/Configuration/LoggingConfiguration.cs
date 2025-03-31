using FwksLabs.Libs.AspNetCore.Options;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class LoggingConfiguration
{
    public static ILogger CreateMinimalLogger()
    {
        var configuration = new Serilog.LoggerConfiguration();

        configuration.WriteTo.Console();

        return configuration.CreateLogger();
    }

    public static Serilog.LoggerConfiguration CreateLoggerConfiguration(LoggingConfigurationOptions options)
    {
        options.Attributes.Add(AppProperties.Otel.ServiceName, options.ServiceName);
        options.Attributes.Add(AppProperties.Otel.ServiceNamespace, options.ServiceNamespace);
        options.Attributes.Add(AppProperties.Otel.ServiceTeam, options.ServiceTeam);

        var configuration = new Serilog.LoggerConfiguration();

        var minimumLevel = LogLevel(options.Logging.MinimumLevel);

        configuration
            .Enrich.FromLogContext()
            .MinimumLevel.Is(minimumLevel);

        foreach (var conf in options.Logging.MinimumLevelOverrides)
            configuration.MinimumLevel.Override(conf.Key, LogLevel(conf.Value));

        configuration.WriteTo.OpenTelemetry(
            endpoint: options.CollectorEndpoint,
            protocol: options.CollectorEndpointProtocol.AsEnum<OtlpProtocol>(),
            restrictedToMinimumLevel: minimumLevel,
            resourceAttributes: options.Attributes);

        return configuration;

        static LogEventLevel LogLevel(string level) => level.AsEnum<LogEventLevel>(LogEventLevel.Warning);
    }
}