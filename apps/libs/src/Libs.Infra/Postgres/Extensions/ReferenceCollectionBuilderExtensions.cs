using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class ReferenceCollectionBuilderExtensions
{
    public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithKeyedConstraintName<TEntity, TRelatedEntity>(
        this ReferenceCollectionBuilder<TEntity, TRelatedEntity> referenceCollectionBuilder, params string[] keys) where TEntity : class where TRelatedEntity : class
    {
        return referenceCollectionBuilder.HasConstraintName($"FK_{string.Join('_', keys)}");
    }
}