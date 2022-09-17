using System;
using System.Collections.Generic;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;
using Spirebyte.Shared.Changes.ValueObjects;

namespace Spirebyte.Services.Activities.Application.ProjectGroups.Events.External;

[Message("projects", "project_group_updated", "activities.project_group_updated")]
public class ProjectGroupUpdated : IEvent
{
    public Guid Id { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> UserIds { get; set; }

    public Change[] Changes { get; set; }
}