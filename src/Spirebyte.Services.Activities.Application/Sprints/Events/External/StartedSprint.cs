using Spirebyte.Framework.Shared.Abstractions;
using Spirebyte.Framework.Shared.Attributes;

namespace Spirebyte.Services.Activities.Application.Sprints.Events.External;

[Message("sprints", "started_sprint", "activities.started_sprint")]
public record StartedSprint(string SprintId, string ProjectId) : IEvent;