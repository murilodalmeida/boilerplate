using System;
using FluentValidation;
using FwksLabs.Libs.Core.Constants;

namespace FwksLabs.Libs.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
        ruleBuilder
            .Must(value => value is not null && AppRegex.PhoneNumber().IsMatch(value))
            .WithMessage("{PropertyName} must be a valid phone number starting with + country code plus number.");

    public static IRuleBuilderOptions<T, DateTime> NotInThePast<T>(this IRuleBuilder<T, DateTime> ruleBuilder) =>
        ruleBuilder
            .Must(date => date < DateTime.UtcNow)
            .WithMessage("{PropertyName} must a date in the present, not in the past.");

    public static IRuleBuilderOptions<T, DateTime> NotInTheFuture<T>(this IRuleBuilder<T, DateTime> ruleBuilder) =>
        ruleBuilder
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("{PropertyName} must a date in the past, not in the future.");

    public static IRuleBuilderOptions<T, DateTime> BeforeDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder, Func<T, DateTime> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"{{PropertyName}} must be before {dateSelector(entity):yyyy-MM-dd}.");

    public static IRuleBuilderOptions<T, DateTime> AfterDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder, Func<T, DateTime> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"{{PropertyName}} must be after {dateSelector(entity):yyyy-MM-dd}.");
}