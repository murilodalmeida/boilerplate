using System;
using System.Collections.Generic;
using FwksLabs.Boilerplate.Core.Enums;
using FwksLabs.Boilerplate.Core.ValueObjects;
using FwksLabs.Libs.Core.Contracts.Common;

namespace FwksLabs.Boilerplate.Core.Entities;

public sealed class PostEntity : Entity<Guid>
{
    public required AuthorValueObject Author { get; set; }
    public PostModerationStatus ModerationStatus { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CommentEntity> Comments { get; set; } = [];
}