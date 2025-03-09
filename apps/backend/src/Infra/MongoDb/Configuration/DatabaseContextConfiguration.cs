using FwksLabs.Boilerplate.Core.Settings;
using FwksLabs.Boilerplate.Infra.Abstractions;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.MongoDb.Configuration;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb.Configuration;

public static class DatabaseContextConfiguration
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, AppSettings appSettings)
    {
        typeof(IInfra).ConfigureFromType<ITypeConfiguration>();

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        return services
            .AddScoped<IMongoClient>(_ => new MongoClient(appSettings.MongoDb.ConnectionString))
            .AddScoped<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<IMongoClient>().GetDatabase(appSettings.MongoDb.Database.Camelize())));
    }
}