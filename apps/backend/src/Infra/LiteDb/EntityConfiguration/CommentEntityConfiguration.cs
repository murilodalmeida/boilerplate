using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Infra.LiteDb.Configuration;
using LiteDB;

namespace FwksLabs.Boilerplate.Infra.LiteDb.EntityConfiguration;

public sealed class CommentEntityConfiguration : BaseEntityConfiguration<CommentEntity>, ITypeConfiguration
{
    public override void Extend(EntityBuilder<CommentEntity> mapper)
    {
        mapper
            .Ignore(x => x.Id)
            .Ignore(x => x.PostId)
            .Ignore(x => x.Post);
    }
}