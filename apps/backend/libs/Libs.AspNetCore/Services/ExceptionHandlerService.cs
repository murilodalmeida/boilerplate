using System;
using System.Net.Mime;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Libs.AspNetCore.Services;

public sealed class ExceptionHandlerService(ILogger<ExceptionHandlerService> logger) : IExceptionHandlerService
{
    private readonly ILogger<ExceptionHandlerService> logger = logger;

    public async Task HandleAsync<TException>(HttpContext context, TException exception) where TException : Exception
    {
        logger.LogError(exception, "An unexpected error occurred.");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await context.Response.WriteAsJsonAsync(AppProblems.InternalServerError());
    }
}