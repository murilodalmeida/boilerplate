using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Infra.LiteDb.Extensions;
using LiteDB;

namespace FwksLabs.Boilerplate.Infra.LiteDb;

public sealed class DatabaseContext(LiteDatabase database) : IDatabaseContext
{
    private ILiteCollection<PostEntity>? posts;
    private ILiteCollection<CommentEntity>? comments;

    public ILiteCollection<PostEntity> Posts => posts ??= database.GetNamedCollection<PostEntity>();
    public ILiteCollection<CommentEntity> Comments => comments ??= database.GetNamedCollection<CommentEntity>();
}