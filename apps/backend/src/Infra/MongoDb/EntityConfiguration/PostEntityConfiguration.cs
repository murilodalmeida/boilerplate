using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Infra.MongoDb.Configuration;
using MongoDB.Bson.Serialization;

namespace FwksLabs.Boilerplate.Infra.MongoDb.EntityConfiguration;

public sealed class PostEntityConfiguration : ITypeConfiguration
{
    public PostEntityConfiguration()
    {
        BsonClassMap.RegisterClassMap<PostEntity>(mapper =>
        {
            mapper.AutoMap();
        });
    }
}