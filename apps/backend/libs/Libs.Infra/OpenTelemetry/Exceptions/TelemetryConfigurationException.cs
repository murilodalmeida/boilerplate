using System;

namespace FwksLabs.Libs.Infra.OpenTelemetry.Exceptions;

public sealed class TelemetryConfigurationException(string message) : Exception(message);