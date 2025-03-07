using System.IO.Compression;
using System.Linq;
using FwksLabs.Libs.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class JsonSerializerConfiguration
{
    public static IHostApplicationBuilder ConfigureJsonSerializer(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(options =>
               JsonSerializerOptionsConfiguration.Configure(options.SerializerOptions));

        return builder;
    }
}

public static class CompressionConfiguration
{
    public static IHostApplicationBuilder ConfigureResponseCompression(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddResponseCompression()
            .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
            .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
            .Configure<ResponseCompressionOptions>(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/json", "application/problem+json"]);
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

        return builder;
    }
}
