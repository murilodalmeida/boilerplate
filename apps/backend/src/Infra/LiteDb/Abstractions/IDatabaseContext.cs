using FwksLabs.Boilerplate.Core.Entities;
using LiteDB;

namespace FwksLabs.Boilerplate.Infra.LiteDb.Abstractions;

public interface IDatabaseContext
{
    public ILiteCollection<PostEntity> Posts { get; }
    public ILiteCollection<CommentEntity> Comments { get; }
}