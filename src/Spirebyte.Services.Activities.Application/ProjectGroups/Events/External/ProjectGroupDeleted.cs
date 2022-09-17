using System;
using System.Collections.Generic;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.ProjectGroups.Events.External;

[Message("projects", "project_group_deleted", "activities.project_group_deleted")]
public class ProjectGroupDeleted : IEvent
{
    public Guid Id { get; set; }
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> UserIds { get; set; }
}