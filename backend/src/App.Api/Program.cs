using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FwksLabs.Boilerplate.Core.Abstractions;
using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Libs.AspNetCore.Options;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Encoders;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Options;
using FwksLabs.Libs.Infra.HealthCheck.Configuration;
using FwksLabs.Libs.Infra.HealthCheck.Options;
using FwksLabs.Libs.Infra.Observability.Configuration;
using FwksLabs.Libs.Infra.Observability.Options;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Core;
using ZiggyCreatures.Caching.Fusion;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var appSettings = ConfigureAppSettings();

    ConfigureLogger();
    ConfigureCorsPolicies();
    ConfigureFluentValidation();
    ConfigureObservability();
    ConfigureHealthChecks();
    ConfigureEncoders();

    builder
        .ConfigureJsonSerializer()
        .ConfigureResponseCompression();

    builder.Services
        .AddAppContext()
        .AddOpenApi()
        .AddVersioning()
        .AddHttpClient()
        .AddHttpContextAccessor()
        .AddExceptionHandlerService()
        .AddInfraDependencies(appSettings);

    var app = builder.Build();

    if (appSettings.IsDevelopment)
        ConfigureScalar();

    app
        .UseCorrelationId()
        .UseSerilogRequestLogging()
        .UseHttpsRedirection()
        .UseCors()
        .UseResponseCompression()
        .UseExceptionHandlerService()
        .UseHealthCheckEndpoints();

    app.MapEndpointGroups<Program>();

    Log.Logger.Information("Starting the application");

    await app.RunAsync();

    AppSettings ConfigureAppSettings()
    {
        var settings = builder.Configuration.Get<AppSettings>()!;

        settings.IsDevelopment = builder.Environment.IsDevelopment();

        builder.Services.AddSingleton(settings);

        return settings;
    }

    void ConfigureLogger()
    {
        var logger = LoggingConfiguration.CreateLoggerConfiguration(BuildOptions());

        if (builder.Environment.IsDevelopment())
            logger.WriteTo.Console();

        Log.Logger = logger.CreateLogger();

        builder.Services.AddLogging().AddSerilog();

        LoggingConfigurationOptions BuildOptions() => new()
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
        };
    }

    void ConfigureCorsPolicies()
    {
        builder.Services.AddCorsPolicies([.. appSettings.Security.CorsPolicies.Select(BuildOptions)]);

        CorsPolicyOptions BuildOptions(KeyValuePair<string, CorsPolicySettings> cors) =>
            new(cors.Key,
                cors.Value.AllowedHeaders,
                cors.Value.AllowedMethods,
                cors.Value.AllowedOrigins,
                cors.Value.ExposedHeaders);
    }

    void ConfigureFluentValidation()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new(appSettings.Localization.DefaultCulture);

        builder.Services
            .AddValidatorsFromAssembly(typeof(Program).Assembly)
            .AddValidatorsFromAssembly(typeof(ICore).Assembly);
    }

    void ConfigureObservability()
    {
        var observabilityOptions = BuildOptions();

        builder.Services
            .AddOpenTelemetry()
            .ConfigureTracing(observabilityOptions)
            .ConfigureMetrics(observabilityOptions);

        ObservabilityOptions BuildOptions() => new()
        {
            ServiceName = appSettings.ServiceInfo.Name,
            ServiceNamespace = appSettings.ServiceInfo.Namespace,
            ServiceVersion = appSettings.ServiceInfo.Version,
            ServiceInstanceId = Environment.MachineName,
            Attributes = appSettings.Observability.Attributes,
            CollectorEndpoint = appSettings.Observability.CollectorEndpoint,
            CollectorEndpointProtocol = appSettings.Observability.CollectorEndpointProtocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc),
            TemporalityPreference = appSettings.Observability.Metrics.TemporalityPreference.AsEnum<MetricReaderTemporalityPreference>(MetricReaderTemporalityPreference.Delta),
            ActivitySources = [new(FusionCacheDiagnostics.ActivitySourceName)],
            Meters = [FusionCacheDiagnostics.MeterName],
            IncomingRequestPathsFilter = appSettings.Observability.Tracing.IncomingRequestPathsFilter,
            IncomingRequestFilter = null,
            OutgoingRequestPathsFilter = appSettings.Observability.Tracing.OutgoingRequestPathsFilter,
            OutgoingRequestFilter = null,
            TracingProcessors = []
        };
    }

    void ConfigureHealthChecks()
    {
        builder.Services
            .AddPostgresHealthCheck(BuildDatabaseOptions(appSettings.Postgres))
            .AddMongoDbHealthCheck(BuildDatabaseOptions(appSettings.MongoDb))
            .AddLiteDbHealthCheck(BuildDatabaseOptions(appSettings.LiteDb))
            .AddRedisHealthCheck(BuildDatabaseOptions(appSettings.Redis))
            .AddHttpServiceHealthChecks([.. appSettings.Services.Select(BuildServiceOptions)]);

        HealthCheckDependencyOptions BuildDatabaseOptions(ConnectionStringOptions options) =>
            new(string.Empty, options.BuildConnectionString()) { Tags = [AppProperties.HealthCheck.TagsTypeDatabase] };

        HealthCheckDependencyOptions BuildServiceOptions(KeyValuePair<string, HttpServiceDependencySettings> service) =>
            new(service.Key.Kebaberize(), service.Value.Url)
            {
                IsCritical = service.Value.Critical,
                LivenessPath = service.Value.LivenessPath,
                TimeoutInSeconds = service.Value.TimeoutInSeconds,
                Tags = [
                    service.Value.IsExternal
                        ? AppProperties.HealthCheck.TagsTypeExternalHttpService
                        : AppProperties.HealthCheck.TagsTypeInternalHttpService
                ]
            };
    }

    void ConfigureEncoders()
    {
        Base62Encoder.OverwriteCharacterSet(appSettings.Security.Encoders.Base62Alphabet);
    }

    void ConfigureScalar()
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options => options
            .WithTitle(appSettings.ServiceInfo.Name.Titleize()));
    }

}
catch (Exception ex)
{
    GetLogger().Fatal(ex, "Application unexpectedly failed to start.");

    ILogger GetLogger()
    {
        return Log.Logger.GetType() == typeof(Logger)
                ? Log.Logger
                : LoggingConfiguration.CreateMinimalLogger();
    }
}
finally
{
    await Log.CloseAndFlushAsync();
}