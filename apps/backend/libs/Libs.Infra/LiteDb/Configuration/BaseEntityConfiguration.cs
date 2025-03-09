using System;
using FwksLabs.Libs.Core.Contracts.Common;
using LiteDB;

namespace FwksLabs.Libs.Infra.LiteDb.Configuration;

public class BaseEntityConfiguration<TEntity>
    where TEntity : BaseEntity<Guid>
{
    protected BaseEntityConfiguration()
    {
        Configure(BsonMapper.Global.Entity<TEntity>());
    }

    public virtual void Configure(EntityBuilder<TEntity> mapper)
    {
        mapper
             .Id(x => x.Id)
             .Ignore(x => x.ReferenceId);

        Extend(mapper);
    }

    public virtual void Extend(EntityBuilder<TEntity> mapper) { }
}