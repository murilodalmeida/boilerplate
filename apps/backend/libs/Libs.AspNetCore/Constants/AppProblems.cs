using System.Net;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class AppProblems
{
    // 4XX

    public static ProblemDetails NotFound(string message) =>
        Problem(HttpStatusCode.NotFound, "Resource Not Found", message);

    public static ProblemDetails Unauthorized() =>
        Problem(HttpStatusCode.Unauthorized, "Unauthorized", "The request does not have valid authentication credentials.");

    public static ProblemDetails Unauthorized(string instance) =>
        Problem(HttpStatusCode.Unauthorized, "Unauthorized", "The request does not have valid authentication credentials.", instance);

    public static ProblemDetails BadRequest(string message) =>
        Problem(HttpStatusCode.BadRequest, "Bad Request", message);

    public static ProblemDetails BadRequest(string title, string message) =>
        Problem(HttpStatusCode.BadRequest, title, message);

    public static ProblemDetails UnprocessableEntity(string message) =>
        Problem(HttpStatusCode.UnprocessableEntity, "Unprocessable Entity", message);

    public static ProblemDetails UnprocessableEntity(string title, string message) =>
        Problem(HttpStatusCode.UnprocessableEntity, title, message);

    // 5XX
    public static ProblemDetails InternalServerError() =>
        InternalServerError("An unexpected error occurred while processing the request.");

    public static ProblemDetails InternalServerError(string message) =>
        Problem(HttpStatusCode.InternalServerError, "Internal Server Error", message);

    public static ProblemDetails Problem(HttpStatusCode statusCode, string title, string detail, string? instance = null) =>
        new()
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = instance,
            Type = $"https://docs.fwkslabs.com/errors/{statusCode.ToString().Kebaberize().ToLower()}"
        };
}