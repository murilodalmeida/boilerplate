using FwksLabs.Libs.Core.Contracts.Common;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Configuration;

public abstract class BaseEntityConfiguration<TPrimaryKeyType, TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TPrimaryKeyType>
    where TPrimaryKeyType : struct
{
    public virtual string TableName { get; } = typeof(TEntity).Name.PluralizeEntity();
    public virtual string SchemaName { get; } = "App";

    public virtual void Extend(EntityTypeBuilder<TEntity> builder) { }

    public virtual void ConfigureIds(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName($"PK_{TableName}");

        builder
            .HasIndex(x => x.ReferenceId)
            .IsUnique()
            .HasDatabaseName($"IX_UK_{TableName}");
    }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .ToTable(TableName, SchemaName);

        ConfigureIds(builder);

        Extend(builder);
    }
}