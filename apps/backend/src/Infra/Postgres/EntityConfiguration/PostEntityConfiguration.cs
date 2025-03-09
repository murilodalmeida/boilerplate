using System;
using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Boilerplate.Infra.Postgres.EntityConfiguration;

public sealed class PostEntityConfiguration : BaseEntityConfiguration<Guid, PostEntity>, ITypeConfiguration
{
    public override void Extend(EntityTypeBuilder<PostEntity> builder)
    {
        builder
            .OwnsOne(x => x.Author).ToJson();
    }
}