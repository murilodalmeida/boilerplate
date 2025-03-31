using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class ValidationExtensions
{
    public static Task<ValidationResult> ValidateAsync<TRequest>(
        this TRequest request, IValidator<TRequest> validator, CancellationToken cancellationToken) where TRequest : class, IRequest =>
            validator.ValidateAsync(request, cancellationToken);

    public static IResult ToResponse(this ValidationResult validationResult) =>
        AppResponses.ValidationErrors(validationResult);
}