using System;
using System.Linq;
using FluentValidation;
using FwksLabs.Boilerplate.App.Api.Configuration;
using FwksLabs.Boilerplate.Core.Abstractions;
using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.Options;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.HealthCheck.Configuration;
using FwksLabs.Libs.Infra.HealthCheck.Options;
using FwksLabs.Libs.Infra.OpenTelemetry.Configuration;
using FwksLabs.Libs.Infra.OpenTelemetry.Options;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Core;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var appSettings = ConfigureAppSettings();

    ConfigureLogger();
    ConfigureCorsPolicies();
    ConfigureValidators();
    ConfigureTelemetry();
    ConfigureHealthCheck();

    builder
        .ConfigureJsonSerializer()
        .ConfigureResponseCompression();

    builder.Services
        .AddOpenApi()
        .AddVersioning()
        .AddHttpClient()
        .AddHttpContextAccessor()
        .AddExceptionHandlerService()
        .AddInfraDependencies(appSettings);

    var app = builder.Build();

    if (appSettings.IsDevelopment)
        app.MapOpenApi(appSettings);

    app
        .UseCorrelationId()
        .UseSerilogRequestLogging()
        .UseHttpsRedirection()
        .UseCors()
        .UseResponseCompression()
        .UseExceptionHandlerService()
        .UseHealthCheckEndpoints();

    app.MapEndpoints();

    Log.Logger.Information("Starting the application");

    await app.RunAsync();

    AppSettings ConfigureAppSettings()
    {
        var settings = builder.Configuration.Get<AppSettings>()!;

        settings.IsDevelopment = builder.Environment.IsDevelopment();

        builder.Services
            .AddAppContext()
            .AddSingleton(settings);

        return settings;
    }

    void ConfigureLogger()
    {
        var logger = ObservabilityConfiguration.CreateLoggerConfiguration(new()
        {
            ServiceName = appSettings.ServiceInfo.Name,
            ServiceNamespace = appSettings.ServiceInfo.Namespace,
            ServiceTeam = appSettings.ServiceInfo.Team,
            CollectorEndpoint = appSettings.Observability.CollectorEndpoint,
            CollectorEndpointProtocol = appSettings.Observability.CollectorEndpointProtocol,
            Attributes = appSettings.Observability.Attributes,
            Logging = new()
            {
                MinimumLevel = appSettings.Observability.Logging.MinimumLevel,
                MinimumLevelOverrides = appSettings.Observability.Logging.MinimumLevelOverrides
            }
        });

        if (builder.Environment.IsDevelopment())
            logger.WriteTo.Console();

        Log.Logger = logger.CreateLogger();

        builder.Services.AddLogging().AddSerilog();
    }

    void ConfigureCorsPolicies()
    {
        var policies = appSettings.Security.CorsPolicies
            .Select(x => new CorsPolicyOptions(
                x.Key,
                x.Value.AllowedHeaders,
                x.Value.AllowedMethods,
                x.Value.AllowedOrigins,
                x.Value.ExposedHeaders
            ));

        builder.Services.AddCorsPolicies([.. policies]);
    }

    void ConfigureValidators()
    {
        builder.Services
            .AddValidatorsFromAssembly(typeof(ICore).Assembly)
            .AddValidatorsFromAssembly(typeof(IEndpointValidator).Assembly);
    }

    void ConfigureTelemetry()
    {
        var options = BuildOptions();

        builder.Services
            .AddOpenTelemetry()
            .ConfigureTracing(options)
            .ConfigureMetrics(options);

        TelemetryOptions BuildOptions()
        {
            return new()
            {
                ServiceName = appSettings.ServiceInfo.Name,
                ServiceNamespace = appSettings.ServiceInfo.Namespace,
                ServiceVersion = appSettings.ServiceInfo.Version,
                ServiceInstanceId = Environment.MachineName,
                Attributes = appSettings.Observability.Attributes,
                CollectorEndpoint = appSettings.Observability.CollectorEndpoint,
                CollectorEndpointProtocol = appSettings.Observability.CollectorEndpointProtocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc),
                TemporalityPreference = appSettings.Observability.Metrics.TemporalityPreference.AsEnum<MetricReaderTemporalityPreference>(MetricReaderTemporalityPreference.Delta),
                ActivitySources = [],
                IncomingRequestPathsFilter = appSettings.Observability.Tracing.IncomingRequestPathsFilter,
                IncomingRequestFilter = null,
                OutgoingRequestPathsFilter = appSettings.Observability.Tracing.OutgoingRequestPathsFilter,
                OutgoingRequestFilter = null,
                TracingProcessors = []
            };
        }
    }

    void ConfigureHealthCheck()
    {
        var options = new HealthCheckDependencyOptions
        {
            Name = string.Empty,
            FailureStatus = HealthStatus.Unhealthy,
            IsCritical = true,
            TimeoutInSeconds = 5,
            Tags = []
        };

        builder.Services
            .AddPostgresHealthCheck(options with { Target = appSettings.Postgres.ConnectionString })
            .AddMongoDbHealthCheck(options with { Target = appSettings.MongoDb.ConnectionString })
            .AddLiteDbHealthCheck(options with { Target = appSettings.LiteDb.ConnectionString });

        foreach (var service in appSettings.Services)
        {
            builder.Services
                .AddHttpServiceHealthCheck(options with
                {
                    Name = service.Key.Kebaberize(),
                    Target = service.Value.Url,
                    IsCritical = service.Value.Critical,
                    FailureStatus = service.Value.Critical ? HealthStatus.Unhealthy : HealthStatus.Degraded,
                    LivenessPath = service.Value.LivenessPath,
                    TimeoutInSeconds = service.Value.TimeoutInSeconds,
                    Tags = []
                });
        }
    }
}
catch (Exception ex)
{
    GetLogger().Fatal(ex, "Application unexpectedly failed to start.");

    ILogger GetLogger()
    {
        return Log.Logger.GetType() == typeof(Logger)
                ? Log.Logger
                : ObservabilityConfiguration.CreateMinimalLogger();
    }
}
finally
{
    await Log.CloseAndFlushAsync();
}