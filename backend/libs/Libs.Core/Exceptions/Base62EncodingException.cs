using System;

namespace FwksLabs.Libs.Core.Exceptions;

public class Base62EncodingException(string message, Exception? innerException = null) : Exception(message, innerException);