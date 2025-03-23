using FwksLabs.Libs.Core.Abstractions.Contracts.Common;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Configuration;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    private readonly static string entityName = typeof(TEntity).Name.PluralizeEntity();

    public virtual string TableName { get; } = entityName;
    public virtual string SchemaName { get; } = "App";
    public virtual string PrimaryKeyName { get; set; } = $"PK_{entityName}";

    public virtual void Extend(EntityTypeBuilder<TEntity> builder) { }

    public virtual void ConfigureIds(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName(PrimaryKeyName);
    }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName, SchemaName);

        ConfigureIds(builder);

        Extend(builder);
    }
}