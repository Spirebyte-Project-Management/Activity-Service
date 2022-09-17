using System;
using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Projects.Events.External;

[Message("projects", "project_left", "activities.project_left")]
public class ProjectLeft : IEvent
{
    public string Id { get; set; }

    public Guid UserId { get; set; }
}