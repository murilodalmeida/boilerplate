using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Infra.MongoDb.Abstractions;
using FwksLabs.Libs.Infra.MongoDb.Extensions;
using MongoDB.Driver;

namespace FwksLabs.Boilerplate.Infra.MongoDb;

public sealed class DatabaseContext(IMongoDatabase database) : IDatabaseContext
{
    private readonly IMongoDatabase database = database;

    private IMongoCollection<PostEntity>? posts;
    private IMongoCollection<CommentEntity>? comments;

    public IMongoCollection<PostEntity> Posts => posts ??= database.GetNamedCollection<PostEntity>();
    public IMongoCollection<CommentEntity> Comments => comments ??= database.GetNamedCollection<CommentEntity>();
}