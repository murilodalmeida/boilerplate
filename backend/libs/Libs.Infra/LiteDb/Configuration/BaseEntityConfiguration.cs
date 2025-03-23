using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using LiteDB;

namespace FwksLabs.Libs.Infra.LiteDb.Configuration;

public class BaseEntityConfiguration<TPrimaryKey, TEntity>
    where TPrimaryKey : struct
    where TEntity : IEntity
{
    protected BaseEntityConfiguration()
    {
        Configure(BsonMapper.Global.Entity<TEntity>());
    }

    public virtual void Configure(EntityBuilder<TEntity> mapper)
    {
        mapper
             .Id(x => x.Id);

        Extend(mapper);
    }

    public virtual void Extend(EntityBuilder<TEntity> mapper) { }
}