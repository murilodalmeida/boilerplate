using System;

namespace FwksLabs.Libs.Core.Exceptions;

public sealed class AppConfigurationException(string message) : Exception(message);