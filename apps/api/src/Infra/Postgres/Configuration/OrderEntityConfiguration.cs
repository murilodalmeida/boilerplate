using FwksLabs.Boilerplate.Core.Entities;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Boilerplate.Infra.Postgres.Configuration;

public sealed class OrderEntityConfiguration : BaseEntityConfiguration<OrderEntity>
{
    public override void Extend(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.OwnsMany(x => x.Products).ToJson();

        builder.Ignore(x => x.Total);
    }
}