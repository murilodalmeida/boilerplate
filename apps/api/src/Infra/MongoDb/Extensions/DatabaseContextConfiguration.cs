using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Abstractions;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.MongoDb.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb.Extensions;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, AppSettings appSettings)
    {
        typeof(IInfra).ConfigureFromType<ITypeConfiguration>();

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        if (appSettings.MongoDb.Options.TryGetValue("database", out var database) is false)
            throw new MongoConfigurationException("Database name is required. Check the app settings and try again.");

        services
            .AddScoped<IMongoClient>(_ => new MongoClient(appSettings.MongoDb.BuildMongoDbConnectionString()))
            .AddScoped<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<IMongoClient>().GetDatabase(database.ToString())));

        return services;
    }
}