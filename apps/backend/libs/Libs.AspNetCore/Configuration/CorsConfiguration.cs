using System;
using System.Collections.Generic;
using FwksLabs.Libs.AspNetCore.Options;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsPolicies(
        this IServiceCollection services,
        IReadOnlyCollection<CorsPolicyOptions> policies) =>
        services.AddCors(options =>
        {
            var defaultAdded = false;

            foreach (var policy in policies)
            {
                if (policy.Name.EqualsTo("default"))
                {
                    if (defaultAdded)
                        throw new InvalidOperationException("You can't add default policies twice. Check your configurations and rename the policies.");

                    options.AddDefaultPolicy(BuildPolicy(policy));
                    defaultAdded = true;
                }
                else
                    options.AddPolicy(policy.Name, BuildPolicy(policy));
            }

            Action<CorsPolicyBuilder> BuildPolicy(CorsPolicyOptions options) =>
                o => o.WithHeaders(options.AllowedHeaders)
                    .WithMethods(options.AllowedMethods)
                    .WithOrigins(options.AllowedOrigins)
                    .WithExposedHeaders(options.ExposedHeaders);
        });
}