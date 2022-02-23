using System;
using System.Collections.Generic;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Spirebyte.Services.Activities.Core.ValueObjects;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects")]
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