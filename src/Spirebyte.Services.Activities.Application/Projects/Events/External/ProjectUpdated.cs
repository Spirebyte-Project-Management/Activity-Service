using System;
using System.Collections.Generic;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;
using Spirebyte.Shared.Changes.ValueObjects;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects", "project_updated", "activities.project_updated")]
public class ProjectUpdated : IEvent
{
    public string Id { get; set; }
    public Guid PermissionSchemeId { get; set; }
    public Guid OwnerUserId { get; set; }
    public IEnumerable<Guid> ProjectUserIds { get; set; }
    public IEnumerable<Guid> InvitedUserIds { get; set; }
    public string Pic { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public Change[] Changes { get; set; }
}