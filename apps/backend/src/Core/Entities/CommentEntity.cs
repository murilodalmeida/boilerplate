using System;
using FwksLabs.Boilerplate.Core.ValueObjects;
using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.Core.Entities;

public sealed class CommentEntity : Entity<Guid>
{
    public Guid PostId { get; set; }
    public required AuthorValueObject Author { get; set; }
    public required string Content { get; set; }
}