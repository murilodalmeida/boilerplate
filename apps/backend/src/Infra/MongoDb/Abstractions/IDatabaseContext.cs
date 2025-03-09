using FwksLabs.Boilerplate.Core.Entities;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;

public interface IDatabaseContext
{
    public IMongoCollection<PostEntity> Posts { get; }
    public IMongoCollection<CommentEntity> Comments { get; }
}