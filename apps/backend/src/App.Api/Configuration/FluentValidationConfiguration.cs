using FluentValidation;
using FwksLabs.Boilerplate.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Boilerplate.App.Api.Configuration;

internal static class FluentValidationConfiguration
{
    internal static IServiceCollection AddValidators(
        this IServiceCollection services, AppSettings appSettings)
    {
        ValidatorOptions.Global.LanguageManager.Culture = new(appSettings.Localization.DefaultCulture);

        return services
            .AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}
