using System;

namespace FwksLabs.Libs.AspNetCore.Contracts;

public class AppRequestContext
{
    public Guid CorrelationId { get; protected set; }

    public void SetCorrelationId(string correlationId) => CorrelationId = Guid.Parse(correlationId);
}