using System;

namespace FwksLabs.Libs.Infra.HealthCheck.Configuration;

public sealed class ApplicationStartupException(string? message = null)
    : Exception(message ?? "Failed to start up the app. Check the values and try again.");