using System;
using System.Collections.Generic;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Spirebyte.Services.Activities.Application.ProjectGroups.Events.External;

[Message("projects")]
public class ProjectGroupDeleted : IEvent
{
    public Guid Id { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; } 
    public IEnumerable<Guid> UserIds { get; set; }
}