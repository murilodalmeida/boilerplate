using System;

namespace FwksLabs.Libs.Core.Contracts.Common;

public abstract class Entity<TPrimaryKeyType> where TPrimaryKeyType : struct
{
    protected Entity()
    {
        if (typeof(TPrimaryKeyType) == typeof(Guid))
        {
            Id = (TPrimaryKeyType)(object)Guid.CreateVersion7();
        }
    }

    public virtual TPrimaryKeyType Id { get; protected set; }
    public virtual Guid ReferenceId { get; protected set; } = Guid.NewGuid();
}