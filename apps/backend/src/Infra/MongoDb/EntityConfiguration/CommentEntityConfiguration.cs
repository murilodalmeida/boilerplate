using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Infra.MongoDb.Configuration;
using MongoDB.Bson.Serialization;

namespace FwksLabs.Boilerplate.Infra.MongoDb.EntityConfiguration;

public sealed class CommentEntityConfiguration : ITypeConfiguration
{
    public CommentEntityConfiguration()
    {
        BsonClassMap.RegisterClassMap<CommentEntity>(mapper =>
        {
            mapper.AutoMap();
            mapper.UnmapProperty(x => x.PostId);
            mapper.UnmapProperty(x => x.Post);
        });
    }
}